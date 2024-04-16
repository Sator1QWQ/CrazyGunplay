using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class RPGBullet : Bullet
{
    public RPGBullet(Weapon ownerWeapon, int bulletId, int gunId) : base(ownerWeapon, bulletId, gunId)
    {
    }

    public override void DoAttackAction(PlayerEntity playerEntity)
    {
        throw new System.NotImplementedException();
    }

    public override void Fly()
    {
        throw new System.NotImplementedException();
    }
}
