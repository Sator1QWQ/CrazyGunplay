using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗状态改变
/// </summary>
public class BattleStateChangeArgs : GameEventArgs
{
    public static readonly int EventId = typeof(BattleStateChangeArgs).GetHashCode();

    public override int Id => EventId;

    public BattleState State { get; private set; }

    public static BattleStateChangeArgs Create(int state)
    {
        BattleStateChangeArgs e = ReferencePool.Acquire<BattleStateChangeArgs>();
        e.State = (BattleState)state;
        return e;
    }

    public override void Clear()
    {
        State = BattleState.None;
    }
}
