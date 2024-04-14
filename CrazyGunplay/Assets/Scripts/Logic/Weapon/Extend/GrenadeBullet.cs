using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using GameFramework.Entity;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class GrenadeBullet : Bullet
{
    private GrenadeRangeType rangeType;
    private float radius;
    private float delay;
    private float tempTime;

    public GrenadeBullet(Weapon ownerWeapon, int bulletId, int gunId) : base(ownerWeapon, bulletId, gunId)
    {
        delay = Config.Get<float>("Grenade", gunId, "delay");
        rangeType = (GrenadeRangeType)Config.Get<int>("Grenade", gunId, "rangeType");
        radius = Config.Get<float>("Grenade", gunId, "radius");
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

    public override bool CustomCheckHide()
    {
        if (tempTime >= delay)
        {
            tempTime = 0;
            Boom();
            return true;
        }
        return false;
    }

    public override void OnHitPlayer(PlayerEntity entity)
    {
        Boom(); //击中玩家直接爆炸
    }

    //爆炸
    private void Boom()
    {
        if (rangeType == GrenadeRangeType.Circle)
        {
            IEntityGroup group = Module.Entity.GetEntityGroup("Player");
            IEntity[] entityArr = group.GetAllEntities();
            for (int i = 0; i < entityArr.Length; i++)
            {
                Entity entity = Module.Entity.GetEntity(entityArr[i].Id);
                float dis = Vector3.Distance(BulletEntityList[0].Entity.transform.position, entity.transform.position);
                if (dis <= radius)
                {
                    Vector3 dir = (entity.transform.position - BulletEntityList[0].Entity.transform.position).normalized;
                    PlayerEntity player = entity.Logic as PlayerEntity;

                    //同步数据
                    BulletHitEventArgs args = BulletHitEventArgs.Create(player.Data.PlayerId, OwnerWeapon.Id);
                    Module.Event.FireNow(this, args);

                    player.GetDamage(GetHitType.HitToFly, dir);
                }
            }
        }
    }
}
