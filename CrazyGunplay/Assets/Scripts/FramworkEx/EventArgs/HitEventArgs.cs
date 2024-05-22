using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 击中事件
/// </summary>
public class HitEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(HitEventArgs).GetHashCode();

    public override int Id => EventId;

    public HitData Data { get; private set; }

    public static HitEventArgs Create(HitData hitData)
    {
        HitEventArgs e = ReferencePool.Acquire<HitEventArgs>();
        e.Data = hitData;
        return e;
    }

    public override void Clear()
    {
        Data = default;
    }
}
