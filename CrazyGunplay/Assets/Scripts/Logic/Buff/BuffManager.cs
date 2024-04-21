using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//每个角色都有一个manager
public class BuffManager : IReference
{
    private Dictionary<int, Buff> mBuffDic = new Dictionary<int, Buff>();

    public void AddBuff(PlayerEntity player, int buffId, float duration, float buffValue)
    {
        //buff已经有的话，则重置buff时间
        if(mBuffDic.ContainsKey(buffId))
        {
            mBuffDic[buffId].ResetBuffTime(duration);
            return;
        }

        Buff buff = ReferencePool.Acquire<Buff>();
        mBuffDic.Add(buffId, buff);
        buff.InitData(player, buffId, duration, buffValue);
        buff.OnBuffFirstStart();
    }

    public void Clear()
    {
        mBuffDic.Clear();
    }
}
