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
    private class VelocityData
    {
        public string id;
        public Vector3 v;
        public float endTime;
        public bool useGravity;
    }

    [SerializeField] private Vector3 leftRaycastOffset;    //左脚射线偏移
    [SerializeField] private Vector3 rightRaycastOffset;    //右脚射线偏移

    private Rigidbody mBody;
    private List<VelocityData> mVelocityDataList = new List<VelocityData>();  //速度数据列表

    private Vector3 v0;
    private Vector3 vt;
    private float upTime; //上升经过的时间
    private float gravityTime;    //只受重力经过的时间

    public bool useGravity;
    private bool seetingUseGravity;

    [HideInInspector] public bool IsAir { get; private set; }
    
    private void Awake()
    {
        mBody = GetComponent<Rigidbody>();
        mBody.useGravity = false;
        seetingUseGravity = useGravity;
    }

    /// <summary>
    /// 添加一个速度，如果time不填的话，需要每帧添加
    /// 如果time填了，则这个速度持续time秒
    /// </summary>
    /// <param name="v">速度</param>
    /// <param name="time"></param>
    /// <param name="useGravity">是否使用重力，后面添加的数据会把前面的重力选项覆盖掉</param>
    public void AddVelocity(string id, Vector3 v, float time = -1, bool useGravity = true)
    {
        float endTime = time != -1 ? Time.time + time : -1;
        mVelocityDataList.Add(new VelocityData() {id = id, v = v, useGravity = useGravity, endTime = endTime });
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
            AddVelocity("UpGravity", vt);
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
                AddVelocity("DownGravity", vt);
            }
        }
    }

    /// <summary>
    /// 恢复重力从速度0开始
    /// </summary>
    private void ResetGravity()
    {
        vt = Vector3.zero;
        v0 = Vector3.zero;
        upTime = 0;
        gravityTime = 0;
    }

    //每帧重新计算速度
    private void DoNormal()
    {
        mBody.velocity = Vector3.zero;

        int dataCount = mVelocityDataList.Count;
        for (int i = 0; i < dataCount; i++)
        {
            VelocityData data = mVelocityDataList[i];
            
            //-1表示下一帧结束
            if(data.endTime == -1)
            {
                data.endTime = Time.time + 0.000001f;
            }
            if(Time.time > data.endTime)
            {
                bool useGravityFlag = false;
                for (int j = 0; j < mVelocityDataList.Count; j++)
                {
                    if (mVelocityDataList[i].useGravity)
                    {
                        useGravityFlag = true;
                    }
                }

                //如果不使用重力，则在结束时还原重力，比如dush，jump + dush结束时重力不还原的话，人物会继续上升
                //全都不用重力时，才需要还原重力，比如dush + jump，结束时jump是需要重力的
                if (!useGravityFlag)
                {
                    ResetGravity();
                }
                mVelocityDataList.Remove(data);
                dataCount = mVelocityDataList.Count;
                i--;
                useGravity = seetingUseGravity;
                continue;
            }
            if(!data.useGravity)
            {
                useGravity = false;
                ResetGravity();
            }
            mBody.velocity += data.v;
        }
    }

    /// <summary>
    /// 停止某个速度
    /// </summary>
    /// <param name="id"></param>
    public void StopVelocity(string id)
    {
        for(int i = 0; i < mVelocityDataList.Count; i++)
        {
            if(mVelocityDataList[i].id.Equals(id))
            {
                mVelocityDataList.RemoveAt(i);
                i--;
            }
        }
    }
}
