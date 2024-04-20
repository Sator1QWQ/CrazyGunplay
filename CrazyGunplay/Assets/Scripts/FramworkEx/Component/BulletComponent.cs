using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using GameFramework.ObjectPool;
using System;
using GameFramework.Event;

/*
* 作者：
* 日期：
* 描述：
		子弹模块
*/
public class BulletComponent : GameFrameworkComponent
{
    private class BulletData
    {
        public int id;
        public Vector3 startPos;
        public Vector3 dire;
    }

    private List<Bullet> bulletList = new List<Bullet>();   //所有子弹字典，id为实体id
    private List<Bullet> flyingList = new List<Bullet>();   //飞行中的子弹的列表

    private void Start()
    {
        Module.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
    }

    //show成功的话会发送事件
    private void OnShowEntity(object obj, GameEventArgs e)
    {
        ShowEntitySuccessEventArgs showEvent = e as ShowEntitySuccessEventArgs;
        if(!showEvent.EntityLogicType.Equals(typeof(BulletEntity)))
        {
            return;
        }

        BulletEntity entity = showEvent.Entity.Logic as BulletEntity;
        Bullet bullet = (Bullet)showEvent.UserData;
        bullet.AddBulletEntity(entity);
        entity.Gravity.RayCastDistance = GlobalDefine.BULLET_RAY_DISTANCE;  //重力射线长度过短会检测不到地面
        
        //子弹全部加载完，开始飞行
        if(bullet.BulletEntityList.Count == bullet.BulletCount)
        {
            bullet.OnShowBullet();
            StartFly(bullet);
        }
    }

    /// <summary>
    /// 每一次的射击都会生成一个子弹对象
    /// 子弹的生成不需要对象池，因为实体已经实现了对象池
    /// </summary>
	public void ShowBullet(Weapon ownerWeapon, int bulletId, int gunId, Vector3 startPos, Vector3 startDirection)
    {
        //BulletType type = Config.Get<BulletType>("Bullet", bulletId, "bulletType");
        Config_Bullet cfg = NewConfig<Config_Bullet>.Get("Bullet", bulletId);
        BulletType type = cfg.bulletType;

        Bullet bullet = null;
        switch (type)
        {
            case BulletType.Normal:
                bullet = new NormalBullet(ownerWeapon, bulletId, gunId);
                break;
            case BulletType.Grenade:
                bullet = new GrenadeBullet(ownerWeapon, bulletId, gunId);
                break;
            case BulletType.RPG:
                bullet = new RPGBullet(ownerWeapon, bulletId, gunId);
                break;
        }
        bulletList.Add(bullet);
        string assetPath = Config.Get<string>("Bullet", bulletId, "assetPath");
        bullet.InitBullet(startPos, startDirection);

        for (int i = 0; i < bullet.BulletCount; i++)
        {
            int entityId = EntityTool.GetBulletEntityId();
            Module.Entity.ShowEntity<BulletEntity>(entityId, assetPath, "Bullet", bullet);
        }
    }

    /// <summary>
    /// 需要手动隐藏所有实体
    /// </summary>
    /// <param name="bullet"></param>
    public void HideBullet(Bullet bullet)
    {
        for (int i = 0; i < bullet.BulletEntityList.Count; i++)
        {
            EntityLogic logic = bullet.BulletEntityList[i];
            Module.Entity.HideEntity(logic.Entity);
        }
        bulletList.Remove(bullet);
        flyingList.Remove(bullet);  //隐藏时肯定是在飞的，所以flyingList里一定会有这个对象
    }

    /// <summary>
    /// 子弹开始飞行
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="pos"></param>
    /// <param name="direction"></param>
    public void StartFly(Bullet bullet)
    {
        flyingList.Add(bullet);
    }

    private void Update()
    {
        for(int i = 0; i < flyingList.Count; i++)
        {
            Bullet bullet = flyingList[i];
            RaycastHit hit = default;
            BulletHideType hideType = bullet.HideType;

            bool isHit = false;
            for(int j = 0; j < bullet.BulletEntityList.Count; j++)
            {
                BulletEntity logic = bullet.BulletEntityList[j];
                //只检测碰撞到玩家和墙
                if (Physics.Raycast(logic.transform.position, logic.LookAt, out hit, 0.35f, LayerMask.GetMask("Player", "MapBorder")) || Physics.Raycast(logic.transform.position, logic.Down, out hit, 0.3f, LayerMask.GetMask("Player", "MapBorder", "Floor")))
                {
                    //击中玩家
                    if((hideType & BulletHideType.HitPlayer) != 0 && hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        PlayerEntity player = hit.transform.GetComponent<PlayerEntity>();
                        //不允许击中自己
                        if (player.PlayerId != bullet.OwnerWeapon.PlayerId)
                        {
                            if(bullet.OnHitPlayer(player))
                            {
                                isHit = true;
                                bullet.DoAttackAction(player);
                                break;  //击中1发就算是全部命中
                            }
                        }
                    }

                    //击中墙壁或地面
                    if((hideType & BulletHideType.HitWall) != 0 && hit.transform.gameObject.layer == (LayerMask.NameToLayer("MapBorder") | LayerMask.NameToLayer("Floor")))
                    {
                        isHit = true;
                        bullet.OnHitWall(hit.transform.gameObject);
                        break;
                    }
                }
            }

            bool customHit = false;
            if(!isHit && bullet.CustomCheckHide())
            {
                customHit = true;
                bullet.DoAttackAction(null);    //自定义模式，没有玩家实体传入
            }

            if(isHit || customHit)
            {
                HideBullet(bullet);
                i--;
            }
            else
            {
                if(bullet.IsFirstFly)
                {
                    bullet.FirstFly();
                    bullet.IsFirstFly = false;
                }
                bullet.Fly();
            }
        }
    }
}
