using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		步枪，手枪子弹
*/
public class NormalBullet : Bullet
{
    public NormalBullet(int bulletId) : base(bulletId)
    {
    }

    public override void Fly()
    {
        for(int i = 0; i < BulletEntityList.Count; i++)
        {
            BulletEntityList[i].Gravity.AddVelocity("bullet", StartDirection.normalized * FlySpeed, -1, false);
        }
    }

    public override void OnCollision(GameObject target)
    {
        PlayerEntity entity = target.GetComponent<PlayerEntity>();
        if(entity != null)
        {
            Debug.Log("发生碰撞了！target==" + target);
            
        }
    }
}
