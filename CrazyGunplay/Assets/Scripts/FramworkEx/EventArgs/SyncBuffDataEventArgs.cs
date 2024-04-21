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
    public static readonly int EventId = typeof(SyncBuffDataEventArgs).GetHashCode();

    public override int Id => EventId;

    public int playerId;
    public BuffData data;

    public static SyncBuffDataEventArgs Create(int playerId, BuffData data)
    {
        SyncBuffDataEventArgs e = ReferencePool.Acquire<SyncBuffDataEventArgs>();
        e.playerId = playerId;
        e.data = data;
        return e;
    }

    public override void Clear()
    {
        playerId = 0;
        data = null;
    }
}
