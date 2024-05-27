using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 命中数据
/// </summary>
public struct HitData
{
    public int dealerId;    //造成命中的id
    /// <summary>
    /// 在初始化时不用设置此参数
    /// </summary>
    public int receiverId;  //被命中的id
    public List<BuffValue> buffList;    //buff列表

    //非击飞可不填
    public GetHitType hitType;  //受击类型 是给receiver的
    public Vector3 direction;   //受击方向 
}

public struct BuffValue
{
    public int buffId;
    public float value;
    public float duration;
}
