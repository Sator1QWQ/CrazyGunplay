using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 切换武器事件
/// </summary>
public class ChangeWeaponEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(ChangeWeaponEventArgs).GetHashCode();

    public override int Id => EventId;

    public int PlayerId { get; private set; }
    public int LastWeaponId { get; private set; }
    public int WeaponId { get; private set; }

    public static ChangeWeaponEventArgs Create(int playerId, int lastWeaponId, int weaponId)
    {
        ChangeWeaponEventArgs e = ReferencePool.Acquire<ChangeWeaponEventArgs>();
        e.PlayerId = playerId;
        e.LastWeaponId = lastWeaponId;
        e.WeaponId = weaponId;
        return e;
    }

    public override void Clear()
    {
        PlayerId = 0;
        LastWeaponId = 0;
        WeaponId = 0;
    }
}
