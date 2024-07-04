using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹数量改变
/// </summary>
public class BulletCountChangeEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(BulletCountChangeEventArgs).GetHashCode();

    public override int Id => EventId;

    public int PlayerId { get; private set; }

    public static BulletCountChangeEventArgs Create(int playerId)
    {
        BulletCountChangeEventArgs e = ReferencePool.Acquire<BulletCountChangeEventArgs>();
        e.PlayerId = playerId;
        return e;
    }

    public override void Clear()
    {
        PlayerId = 0;
    }
}
