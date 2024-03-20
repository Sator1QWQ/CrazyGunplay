using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 死亡事件
/// </summary>
public class PlayerDieEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(PlayerDieEventArgs).GetHashCode();

    public override int Id => EventId;

    /// <summary>
    /// 丢失生命的玩家id
    /// </summary>
    public int PlayerId { get; private set; }

    /// <summary>
    /// 当前剩余血量
    /// </summary>
    public int NowLife { get; private set; }

    public static PlayerDieEventArgs Create(int playerId, int nowLife)
    {
        PlayerDieEventArgs e = ReferencePool.Acquire<PlayerDieEventArgs>();
        e.PlayerId = playerId;
        e.NowLife = nowLife;
        return e;
    }

    //释放对象，必须还原初始状态不然再次使用时数据还在
    public override void Clear()
    {
        PlayerId = 0;
        NowLife = 0;
    }
}
