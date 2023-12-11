using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		重力模拟
*/
[RequireComponent(typeof(Rigidbody))]
public class SimpleGravity : MonoBehaviour
{
    [SerializeField] private Vector3 leftRaycastOffset;    //左脚射线偏移
    [SerializeField] private Vector3 rightRaycastOffset;    //右脚射线偏移

    private Rigidbody mBody;
    private List<Vector3> mVelocityList = new List<Vector3>();  //速度列表

    private Vector3 v0;
    private Vector3 vt;
    private float upTime; //上升经过的时间
    private float gravityTime;    //只受重力经过的时间

    public bool useGravity;
    [HideInInspector] public bool IsAir { get; private set; }
    
    private void Awake()
    {
        mBody = GetComponent<Rigidbody>();
        mBody.useGravity = false;
    }

    /// <summary>
    /// 添加一个速度，需要每帧添加
    /// </summary>
    public void AddVelocity(Vector3 v)
    {
        mVelocityList.Add(v);
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    /// <param name="v"></param>
    public void Jump(Vector3 v)
    {
        v0 = v;
        vt = v;
        upTime = 0;
    }

    private void Update()
    {
        //触碰地面
        RaycastHit hitLeft;
        RaycastHit hitRight;
        if (Physics.Raycast(transform.position + leftRaycastOffset, Vector3.down, out hitLeft, 20, ~LayerMask.GetMask("Player")) && Physics.Raycast(transform.position + rightRaycastOffset, Vector3.down, out hitRight, 20, ~LayerMask.GetMask("Player")))
        {
            float dis = transform.position.y - hitLeft.point.y;
            float dis2 = transform.position.y - hitRight.point.y;
            if (dis <= 0.5f || dis2 <= 0.5f)
            {
                if (IsAir)
                {
                    Debug.Log("触碰到地面了");
                    gravityTime = 0;
                }
                IsAir = false;
            }
            else
            {
                IsAir = true;
            }
        }
        else
        {
            IsAir = true;
        }

        if(useGravity)
        {
            GravityCaculate();
        }

        DoNormal();
    }

    //重力计算
    private void GravityCaculate()
    {
        if (vt.y > 0)
        {
            upTime += Time.deltaTime;
            vt = v0 + GlobalDefine.G * upTime;
            AddVelocity(vt);
            if (IsAir && vt.y >= 0 && vt.y <= 1f)
            {
                Debug.Log("最高点");
                gravityTime = 0;
            }
        }
        else
        {
            if (IsAir)
            {
                Debug.Log("只受重力");
                gravityTime += Time.deltaTime;
                vt = GlobalDefine.G * gravityTime;
                AddVelocity(vt);
            }
        }
    }

    //每帧重新计算速度
    private void DoNormal()
    {
        mBody.velocity = Vector3.zero;
        for (int i = 0; i < mVelocityList.Count; i++)
        {
            mBody.velocity += mVelocityList[i];
        }
        mVelocityList.Clear();
    }
}
