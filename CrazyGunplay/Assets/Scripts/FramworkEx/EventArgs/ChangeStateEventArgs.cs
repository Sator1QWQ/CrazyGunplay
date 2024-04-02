using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态机改变状态时调用
/// </summary>
public class ChangeStateEventArgs<T> : GameEventArgs where T :class
{
    public static readonly int EventId = typeof(ChangeStateEventArgs<T>).GetHashCode();

    public override int Id => EventId;

    public T Owner { get; private set; }
    public StateLayer Layer { get; private set; }
    public StateType LastState { get; private set; }
    public StateType CurState { get; private set; }

    public static ChangeStateEventArgs<T> Create(T owner, StateLayer layer, StateType lastState, StateType curState)
    {
        ChangeStateEventArgs<T> e = ReferencePool.Acquire<ChangeStateEventArgs<T>>();
        e.Owner = owner;
        e.Layer = layer;
        e.LastState = lastState;
        e.CurState = curState;
        return e;
    }

    public override void Clear()
    {
        Owner = null;
        Layer = StateLayer.None;
        LastState = StateType.None;
        CurState = StateType.None;
    }
}
