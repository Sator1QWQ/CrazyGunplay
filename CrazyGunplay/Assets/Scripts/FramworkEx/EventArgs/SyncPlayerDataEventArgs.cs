using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 同步玩家数据事件 lua端调用
/// </summary>
public class SyncPlayerDataEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(SyncPlayerDataEventArgs).GetHashCode();

    public override int Id => EventId;

    public PlayerBattleData Data { get; private set; }

    public static SyncPlayerDataEventArgs Create(PlayerBattleData data)
    {
        SyncPlayerDataEventArgs e = ReferencePool.Acquire<SyncPlayerDataEventArgs>();
        e.Data = data;
        return e;
    }

    public override void Clear()
    {
        Data = null;
    }
}
