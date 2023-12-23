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
    private Dictionary<int, Bullet> bulletDic = new Dictionary<int, Bullet>();  //所有子弹字典，id为实体id
    private List<Bullet> flyingList = new List<Bullet>();   //飞行中的子弹的列表

    private void Start()
    {
        Module.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
    }

    //show成功的话会发送事件，之后再new bullet对象
    private void OnShowEntity(object obj, GameEventArgs e)
    {
        ShowEntitySuccessEventArgs showEvent = e as ShowEntitySuccessEventArgs;
        if(!showEvent.EntityLogicType.Equals(typeof(BulletEntity)))
        {
            return;
        }

        BulletEntity entity = showEvent.Entity.Logic as BulletEntity;
        int bulletId = (int)showEvent.UserData;
        BulletType type = Config.Get<BulletType>("Bullet", bulletId, "bulletType");
        Bullet bullet = null;
        switch (type)
        {
            case BulletType.Normal:
                bullet = new NormalBullet(entity, bulletId);
                break;
            case BulletType.Grenade:
                bullet = new GrenadeBullet(entity, bulletId);
                break;
            case BulletType.RPG:
                bullet = new RPGBullet(entity, bulletId);
                break;
        }
        bulletDic.Add(entity.Entity.Id, bullet);
    }

    /// <summary>
    /// 子弹的生成不需要对象池，因为实体已经实现了对象池
    /// </summary>
	public void ShowBullet(int bulletId)
    {
        string value = Config.Get<string>("Bullet", bulletId, "assetPath");
        int bulleltId = EntityTool.GetBulletEntityId();
        Module.Entity.ShowEntity<BulletEntity>(bulleltId, value, "normal", (object)bulletId);
    }

    /// <summary>
    /// 需要手动隐藏实体
    /// </summary>
    /// <param name="bullet"></param>
    public void HideBullet(Bullet bullet)
    {
        int entityId = bullet.BulletEntity.Entity.Id;
        if (!bulletDic.ContainsKey(entityId))
        {
            Debug.LogError("子弹出现错误！隐藏子弹时子弹不存在字典中 entityId==" + entityId);
            return;
        }

        bulletDic.Remove(entityId);
        Module.Entity.HideEntity(entityId);
    }

    /// <summary>
    /// 子弹开始飞行
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="pos"></param>
    /// <param name="direction"></param>
    public void StartFly(Bullet bullet, Vector3 pos, Vector3 direction)
    {
        bullet.StartFly(pos, direction);
        flyingList.Add(bullet);
    }

    private void Update()
    {
        for(int i = 0; i < flyingList.Count; i++)
        {
            Bullet bullet = flyingList[i];
            RaycastHit hit;

            if(Physics.Raycast(bullet.CurPos, bullet.CurDirection, out hit, 0.2f))
            {
                bullet.OnCollision(hit.transform.gameObject);
            }
            else
            {
                bullet.Fly();
            }
        }
    }
}
