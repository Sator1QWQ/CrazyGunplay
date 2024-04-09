using GameFramework.Entity;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

//战斗场景使用：控制摄像机的缩放
public class CameraController : MonoBehaviour
{
    public float moveSpeed = 1;
    public float scaleSpeed = 1;
    [SerializeField] private float minFov = 30;
    [SerializeField] private float maxFov = 100;
    [SerializeField] private float playerPadding = 1;  //玩家内边距

    private IEntity[] entityArr;
    private Camera battleCam;
    private Vector3 cameraPos;

    private void Start()
    {
        battleCam = GetComponent<Camera>();
        IEntityGroup group = Module.Entity.GetEntityGroup("Player");
        entityArr = group.GetAllEntities();
        cameraPos = new Vector3();
    }

    private void OnEnable()
    {
        Module.Event.Subscribe(UnityGameFramework.Runtime.ShowEntitySuccessEventArgs.EventId, OnShowEntity);
    }

    private void OnDisable()
    {
        Module.Event.Unsubscribe(UnityGameFramework.Runtime.ShowEntitySuccessEventArgs.EventId, OnShowEntity);
    }

    private void OnShowEntity(object sender, GameEventArgs e)
    {
        UnityGameFramework.Runtime.ShowEntitySuccessEventArgs args = e as UnityGameFramework.Runtime.ShowEntitySuccessEventArgs;
        if(!args.Entity.EntityGroup.Name.Equals("Player"))
        {
            return;
        }

        IEntityGroup group = Module.Entity.GetEntityGroup("Player");
        entityArr = group.GetAllEntities();
    }

    private void Update()
    {
        if (Module.PlayerData.State != BattleState.Battle && Module.PlayerData.State != BattleState.Ready)
        {
            return;
        }

        //每帧计算fov
        float maxAngle = 0;
        Vector3 totalPos = new Vector3();

        for (int i = 0; i < entityArr.Length; i++)
        {
            IEntity entity = entityArr[i];
            Entity logic = Module.Entity.GetEntity(entity.Id);
            totalPos += logic.transform.position;

            //计算玩家与摄像机的水平夹角
            Vector3 camToPlayer = logic.transform.position - battleCam.transform.position;
            Vector3 camForward = battleCam.transform.forward;
            float angle = Vector3.Angle(camToPlayer, camForward);
            if(angle > maxAngle)
            {
                maxAngle = angle;
            }
        }

        Vector3 centerPoint = totalPos / entityArr.Length;
        cameraPos.x = centerPoint.x;
        cameraPos.y = centerPoint.y + 3;
        cameraPos.z = battleCam.transform.position.z;
        battleCam.transform.position = Vector3.Lerp(battleCam.transform.position, cameraPos, moveSpeed);

        //计算出的夹角是水平Fov，需要转成垂直Fov
        float hFov = maxAngle * 2;
        float aspectRatio = (float)Screen.width / Screen.height;
        float a = hFov / 2 * Mathf.Deg2Rad;
        float b = Mathf.Tan(a);
        float c = b / aspectRatio;
        float vFov = 2 * Mathf.Atan(c) * Mathf.Rad2Deg + playerPadding;
        vFov = Mathf.Clamp(vFov, minFov, maxFov);
        battleCam.fieldOfView = Mathf.Lerp(battleCam.fieldOfView, vFov, (battleCam.fieldOfView / vFov) * scaleSpeed);
    }
}
