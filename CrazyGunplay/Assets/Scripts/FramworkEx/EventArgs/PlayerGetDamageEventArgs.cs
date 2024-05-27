using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家受伤 由lua端发送
/// </summary>
public class PlayerGetDamageEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(PlayerGetDamageEventArgs).GetHashCode();

    public override int Id => EventId;

    /// <summary>
    /// 被击飞的玩家
    /// </summary>
    public int HitPlayer { get; private set; }

    /// <summary>
    /// 受击类型
    /// </summary>
    public GetHitType HitType { get; private set; }

    /// <summary>
    /// 方向
    /// </summary>
    public Vector3 Direction { get; private set; }

    public static PlayerGetDamageEventArgs Create(int hitPlayer, GetHitType hitType, Vector3 direction)
    {
        PlayerGetDamageEventArgs e = ReferencePool.Acquire<PlayerGetDamageEventArgs>();
        e.HitPlayer = hitPlayer;
        e.HitType = hitType;
        e.Direction = direction;
        return e;
    }

    public override void Clear()
    {
        HitPlayer = 0;
        HitType = GetHitType.No;
        Direction = Vector3.zero;
    }
}
