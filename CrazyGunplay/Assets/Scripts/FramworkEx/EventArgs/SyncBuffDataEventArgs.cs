using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Buff数据同步
/// </summary>
public class SyncBuffDataEventArgs : GameEventArgs
{
    //lua端的buff数据
    public class LuaBuffData
    {
        public int id;
        public float duration;
        public float continueTime;
        public float remainTime;
    }

    public static readonly int EventId = typeof(SyncBuffDataEventArgs).GetHashCode();

    public override int Id => EventId;

    public int playerId;
    public BuffData data;
    public Dictionary<int, LuaBuffData> buffDic = new Dictionary<int, LuaBuffData>();

    public static SyncBuffDataEventArgs Create(int playerId, BuffData data, Dictionary<int, LuaBuffData> buffDic)
    {
        SyncBuffDataEventArgs e = ReferencePool.Acquire<SyncBuffDataEventArgs>();
        e.playerId = playerId;
        e.data = data;
        e.buffDic = buffDic;
        return e;
    }

    public override void Clear()
    {
        playerId = 0;
        data = null;
    }
}
