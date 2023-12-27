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
        int bulletId = (int)showEvent.UserData;
        Bullet bullet = bulletList.Find(b => b.Id == bulletId);
        bullet.AddBulletEntity(entity);
        
        //子弹全部加载完，开始飞行
        if(bullet.BulletEntityList.Count == bullet.BulletCount)
        {
            bullet.OnShowBullet();
            StartFly(bullet);
        }
    }

    /// <summary>
    /// 子弹的生成不需要对象池，因为实体已经实现了对象池
    /// </summary>
	public void ShowBullet(int bulletId, Vector3 startPos, Vector3 startDirection)
    {
        Debug.Log("ShowBullet!");
        string value = Config.Get<string>("Bullet", bulletId, "assetPath");
        BulletType type = Config.Get<BulletType>("Bullet", bulletId, "bulletType");
        Bullet bullet = null;
        switch (type)
        {
            case BulletType.Normal:
                bullet = new NormalBullet(bulletId);
                break;
            case BulletType.Grenade:
                bullet = new GrenadeBullet(bulletId);
                break;
            case BulletType.RPG:
                bullet = new RPGBullet(bulletId);
                break;
        }
        bullet.InitBullet(startPos, startDirection);

        bulletList.Add(bullet);
        for (int i = 0; i < bullet.BulletCount; i++)
        {
            int entityId = EntityTool.GetBulletEntityId();
            Module.Entity.ShowEntity<BulletEntity>(entityId, value, "normal", (object)bulletId);
        }
    }

    /// <summary>
    /// 需要手动隐藏所有实体
    /// </summary>
    /// <param name="bullet"></param>
    private void HideBullet(Bullet bullet)
    {
        for (int i = 0; i < bullet.BulletEntityList.Count; i++)
        {
            EntityLogic logic = bullet.BulletEntityList[i];
            Module.Entity.HideEntity(logic.Entity);
        }
        bulletList.Remove(bullet);
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
            RaycastHit hit;

            EntityLogic logic = bullet.BulletEntityList[0];
            if(Physics.Raycast(logic.transform.position, logic.transform.forward, out hit, 0.2f))
            {
                bullet.OnCollision(hit.transform.gameObject);
                HideBullet(bullet);
                flyingList.Remove(bullet);
                i--;
            }
            else
            {
                bullet.Fly();
            }
        }
    }
}
