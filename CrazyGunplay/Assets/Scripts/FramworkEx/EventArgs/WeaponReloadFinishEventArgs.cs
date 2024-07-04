using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloadFinishEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(WeaponReloadFinishEventArgs).GetHashCode();

    public override int Id => EventId;

    public int PlayerId { get; private set; }

    public static WeaponReloadFinishEventArgs Create(int playerId)
    {
        WeaponReloadFinishEventArgs e = ReferencePool.Acquire<WeaponReloadFinishEventArgs>();
        e.PlayerId = playerId;
        return e;
    }

    public override void Clear()
    {
        PlayerId = 0;
    }
}
