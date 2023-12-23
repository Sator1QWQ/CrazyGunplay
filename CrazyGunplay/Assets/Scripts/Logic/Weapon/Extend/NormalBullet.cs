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
    public NormalBullet(BulletEntity entity, int bulletId) : base(entity, bulletId)
    {
    }

    public override void Fly()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollision(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
