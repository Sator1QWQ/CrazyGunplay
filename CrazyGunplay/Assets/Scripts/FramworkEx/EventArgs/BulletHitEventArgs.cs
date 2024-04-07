using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹击中事件
/// </summary>
public class BulletHitEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(BulletHitEventArgs).GetHashCode();

    public override int Id => EventId;
    
    /// <summary>
    /// 受击玩家
    /// </summary>
    public int GetHitPlayer { get; private set; }
    public int WeaponId { get; private set; }

    public static BulletHitEventArgs Create(int getHitPlayer, int weaponId)
    {
        BulletHitEventArgs e = ReferencePool.Acquire<BulletHitEventArgs>();
        e.GetHitPlayer = getHitPlayer;
        e.WeaponId = weaponId;
        return e;
    }

    public override void Clear()
    {
        GetHitPlayer = 0;
        WeaponId = 0;
    }
}
