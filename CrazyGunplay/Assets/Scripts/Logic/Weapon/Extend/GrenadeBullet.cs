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

    public override void FirstFly()
    {
        for(int i = 0; i < BulletEntityList.Count; i++)
        {
            BulletEntityList[i].Gravity.useGravity = true;
            BulletEntityList[i].Gravity.seetingUseGravity = true;
            BulletEntityList[i].Gravity.AddForce("GrenadeBullet_" + Id, new Vector3(1, 1, 0) * 3);
        }
    }

    public override void Fly()
    {
        Debug.Log("手雷飞行");
    }

    public override bool OnCollision(GameObject target)
    {
        Debug.Log("子弹碰撞到了");
        return true;
    }
}
