using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家被击飞
/// </summary>
public class PlayerBeatFlyEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(PlayerBeatFlyEventArgs).GetHashCode();

    public override int Id => EventId;

    /// <summary>
    /// 被击飞的玩家
    /// </summary>
    public int HitPlayer { get; private set; }

    /// <summary>
    /// 方向
    /// </summary>
    public Vector3 Direction { get; private set; }

    public static PlayerBeatFlyEventArgs Create(int hitPlayer, Vector3 direction)
    {
        PlayerBeatFlyEventArgs e = ReferencePool.Acquire<PlayerBeatFlyEventArgs>();
        e.HitPlayer = hitPlayer;
        e.Direction = direction;
        return e;
    }

    public override void Clear()
    {
    }
}
