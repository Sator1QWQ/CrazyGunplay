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
public class GrenadeBullet : Bullet
{
    public GrenadeBullet(Weapon ownerWeapon, int bulletId, int gunId) : base(ownerWeapon, bulletId, gunId)
    {
    }

    public override void Fly()
    {
        throw new System.NotImplementedException();
    }

    public override bool OnCollision(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
