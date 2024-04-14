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
    private float delay;
    private float tempTime;

    public GrenadeBullet(Weapon ownerWeapon, int bulletId, int gunId) : base(ownerWeapon, bulletId, gunId)
    {
        delay = Config.Get<float>("Grenade", gunId, "delay");
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
        tempTime += Time.deltaTime;
    }

    public override bool HideCheckOnHit()
    {
        if(tempTime >= delay)
        {
            tempTime = 0;
            Debug.Log("手雷爆炸");
            return true;
        }
        return false;
    }
}
