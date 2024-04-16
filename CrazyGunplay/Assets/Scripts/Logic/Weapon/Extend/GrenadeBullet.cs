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
            BulletEntityList[i].Gravity.AddForce("GrenadeBullet_" + Id, (Vector3.up + OwnerWeapon.Entity.PlayerEntity.LookDirection).normalized * 5);
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
            return true;
        }
        return false;
    }

    public override bool OnHitPlayer(PlayerEntity entity) => OwnerWeapon.PlayerId != entity.PlayerId;

    public override void DoAttackAction(PlayerEntity playerEntity)
    {
        //如果打在玩家身上，那么不判断距离，直接算命中该玩家
        if (playerEntity != null)
        {
            Vector3 dir = (playerEntity.transform.position - BulletEntityList[0].Entity.transform.position).normalized;
            SendAttackEvent(playerEntity.PlayerId);
            playerEntity.GetDamage(GetHitType.HitToFly, dir);
            Boom(playerEntity.PlayerId);
        }
        else
        {
            Boom();
        }
    }

    //爆炸
    private void Boom(int ignorePlayer = -1)
    {
        if (rangeType == GrenadeRangeType.Circle)
        {
            IEntityGroup group = Module.Entity.GetEntityGroup("Player");
            IEntity[] entityArr = group.GetAllEntities();
            for (int i = 0; i < entityArr.Length; i++)
            {
                Entity entity = Module.Entity.GetEntity(entityArr[i].Id);
                PlayerEntity player = entity.Logic as PlayerEntity;
                
                //忽略掉某个玩家
                if(player.PlayerId == ignorePlayer)
                {
                    continue;
                }

                //不可击中自己时击中自己，则不会受伤
                if (!CanHitSelf && player.PlayerId == OwnerWeapon.PlayerId)
                {
                    continue;
                }

                float dis = Vector3.Distance(BulletEntityList[0].Entity.transform.position, entity.transform.position);
                if (dis <= radius)
                {
                    Vector3 dir = (entity.transform.position - BulletEntityList[0].Entity.transform.position).normalized;
                    SendAttackEvent(player.PlayerId);
                    player.GetDamage(GetHitType.HitToFly, dir);
                }
            }
        }
    }
}
