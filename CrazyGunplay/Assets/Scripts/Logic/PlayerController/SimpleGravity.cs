﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using System;

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

    private class UpdateData
    {
        public float tempTime;
        public bool isEnd;
        public Action act;
        public Action onFloorAct;

        public UpdateData(Action act)
        {
            this.act = act;
        }
    }

    public Vector3 leftRaycastOffset;    //左脚射线偏移
    public Vector3 rightRaycastOffset;    //右脚射线偏移
    public PhysicMaterial noFrictionMaterial;   //无摩擦力的材质

    private Rigidbody mBody;
    private List<VelocityData> mVelocityDataList = new List<VelocityData>();  //速度数据列表

    private Vector3 v0;
    public Vector3 vt { get; private set; }
    private float upTime; //上升经过的时间
    private float gravityTime;    //只受重力经过的时间

    public bool useGravity;
    [HideInInspector] public bool seetingUseGravity;
    private List<UpdateData> mUpdateActionList = new List<UpdateData>();
    private Collider collid;
    private Vector3 lastPos;
    private Vector3 currentPos;

    private bool isAir;
    public bool IsAir 
    {
        get => isAir;
        private set
        {
            isAir = value;
            OnIsAirChange(isAir);
        }
    }

    /// <summary>
    /// 射线检测长度
    /// </summary>
    public float RayCastDistance { get; set; }

    /// <summary>
    /// 触碰到地面时调用
    /// </summary>
    public event Action OnTouchGround = () => { };

    public event Action<bool> OnIsAirChange = _ => { };
    
    private void Awake()
    {
        mBody = GetComponent<Rigidbody>();
        mBody.useGravity = false;
        seetingUseGravity = useGravity;
        RayCastDistance = GlobalDefine.FLOOR_RAY_DISTANCE;
        collid = GetComponent<Collider>();
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
        AddVelocityInteral(id, v, time, useGravity);
    }

    public void AddForce(string id, Vector3 v, float time = -1)
    {
        UpdateData data = null;
        Jump(v.y);
        float floorTime = 0;
        data = new UpdateData(() =>
        {
            Vector3 newV = new Vector3(v.x, 0, v.z);
            if (time != -1)
            {
                if (data.tempTime >= time)
                {
                    data.isEnd = true;
                }
                data.tempTime += Time.deltaTime;
            }
            if(!isAir)
            {
                floorTime += Time.deltaTime;
                if(floorTime >= 0.5f)
                {
                    data.isEnd = true;
                }
            }
            AddVelocityInteral(id, newV, -1, true);
        });
        data.onFloorAct = () => data.isEnd = true;
        AddUpdateAction(data);
    }

    /// <summary>
    /// 添加一个速度，如果time不填的话，需要每帧添加
    /// 如果time填了，则这个速度持续time秒
    /// </summary>
    /// <param name="v">速度</param>
    /// <param name="time"></param>
    /// <param name="useGravity">是否使用重力，后面添加的数据会把前面的重力选项覆盖掉</param>
    private void AddVelocityInteral(string id, Vector3 v, float time = -1, bool useGravity = true)
    {
        float endTime = time != -1 ? Time.time + time : -1;
        mVelocityDataList.Add(new VelocityData() {id = id, v = v, useGravity = useGravity, endTime = endTime });
    }

    /// <summary>
    /// 竖直向上的力
    /// </summary>
    /// <param name="v"></param>
    public void Jump(float speed)
    {
        v0 = Vector3.up * speed;
        vt = Vector3.up * speed;
        upTime = 0;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < mUpdateActionList.Count; i++)
        {
            if (mUpdateActionList[i].isEnd)
            {
                mUpdateActionList.RemoveAt(i);
                i--;
            }
            else
            {
                mUpdateActionList[i].act();
            }
        }

        lastPos = currentPos;
        currentPos = transform.position;

        //触碰地面
        RaycastHit hitLeft;
        RaycastHit hitRight;
        float rayDis = Mathf.Abs(lastPos.y - currentPos.y) + 0.1f;  //射线检测长度应是动态的
        rayDis = Mathf.Clamp(rayDis, GlobalDefine.FLOOR_RAY_DISTANCE, rayDis);
        if (Physics.Raycast(transform.position + leftRaycastOffset, Vector3.down, out hitLeft, rayDis, LayerMask.GetMask("Floor")) || Physics.Raycast(transform.position + rightRaycastOffset, Vector3.down, out hitRight, rayDis, LayerMask.GetMask("Floor")))
        {
            if (IsAir && vt.y <= 0)
            {
                for (int i = 0; i < mUpdateActionList.Count; i++)
                {
                    mUpdateActionList[i].onFloorAct?.Invoke();
                }
                OnTouchGround();
                ResetGravity();
                gravityTime = 0;
                IsAir = false;
            }
        }
        else
        {
            IsAir = true;
        }

        if(noFrictionMaterial != null)
        {
            collid.material = IsAir ? noFrictionMaterial : null;    //在空中，使用无摩擦力的材质
        }

        if (useGravity)
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
    public void ResetGravity()
    {
        vt = Vector3.zero;
        v0 = Vector3.zero;
        upTime = 0;
        gravityTime = 0;
    }

    /// <summary>
    /// 还原速度
    /// </summary>
    public void ResetVelocity()
    {
        mVelocityDataList.Clear();
        mUpdateActionList.Clear();
        mBody.velocity = Vector3.zero;
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
            if (data.endTime == -1)
            {
                data.endTime = Time.time + 0.000001f;
            }
            if (Time.time > data.endTime)
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
            if (!data.useGravity)
            {
                useGravity = false;
                ResetGravity();
            }
            mBody.velocity += data.v;
        }

        /**velocity碰到建筑时会做减法，比如下面
        mBody.velocity = new Vector3(4, -3, 0); 速度只会变成Vector3(1, 0, 0)*/

    }

    private void AddUpdateAction(UpdateData data)
    {
        mUpdateActionList.Add(data);
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
