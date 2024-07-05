using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 预加载完成事件
/// </summary>
public class PreloadFinishEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(PreloadFinishEventArgs).GetHashCode();

    public override int Id => EventId;
    
    public static PreloadFinishEventArgs Create()
    {
        PreloadFinishEventArgs e = ReferencePool.Acquire<PreloadFinishEventArgs>();
        return e;
    }

    public override void Clear()
    {
    }
}
