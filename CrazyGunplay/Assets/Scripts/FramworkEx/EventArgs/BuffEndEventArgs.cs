using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// buff效果结束
/// </summary>
public class BuffEndEventArgs : GameEventArgs
{
    public int playerId;
    public string key;

    public static readonly int EventId = typeof(BuffEndEventArgs).GetHashCode();

    public override int Id => EventId;

    public static BuffEndEventArgs Create(int playerId, string key)
    {
        BuffEndEventArgs e = ReferencePool.Acquire<BuffEndEventArgs>();
        e.playerId = playerId;
        e.key = key;
        return e;
    }

    public override void Clear()
    {
        playerId = 0;
        key = null;
    }
}
