﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 命中数据
/// </summary>
public struct HitData
{
    public int dealerId;    //造成命中的id
    public int receiverId;  //被命中的id
    public List<BuffValue> buffList;    //buff列表
    public GetHitType hitType;  //受击类型 是给receiver的

    public void SetReceiverId(int id)
    {
        receiverId = id;
    }
}

public struct BuffValue
{
    public int buffId;
    public float value;
    public float continueTime;
}
