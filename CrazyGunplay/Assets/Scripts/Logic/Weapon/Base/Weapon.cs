using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;
using GameFramework.Entity;

/*
* 作者：
* 日期：
* 描述：
		武器类
*/
public abstract class Weapon
{
	public int PlayerId { get; private set; }
	public int Id { get; private set; }

	public PlayerEntity PlayerEntity { get; private set; }
	public WeaponEntity Entity { get; private set; }
	public Config_Weapon Config { get; private set; }
	public int EntityId { get; private set; }

	/// <summary>
	/// 主弹夹，如果武器没有弹药设定，则为-1
	/// </summary>
	public int MainMagazine { get; private set; }

	/// <summary>
	/// 备用弹夹，如果武器没有弹药设定，则为-1
	/// </summary>
	public int SpareMagazine { get; private set; }

	public Weapon(Config_Weapon config, int playerId, int id, PlayerEntity playerEntity)
    {
		PlayerId = playerId;
		Id = id;
		Config = config;
		PlayerEntity = playerEntity;

		//设置初始弹夹数量
		if(config.weaponType == WeaponType.Gun)
        {
			Config_Gun cfg = Config<Config_Gun>.Get("Gun", Id);
			MainMagazine = cfg.mainMag;
			SpareMagazine = cfg.spareMag;
        }
		else if(config.weaponType == WeaponType.Throw)
        {
			Config_Grenade cfg = Config<Config_Grenade>.Get("Grenade", Id);
			MainMagazine = cfg.mainMag;
			SpareMagazine = 0;
		}
		else
        {
			MainMagazine = -1;
			SpareMagazine = -1;
        }
    }

	/// <summary>
	/// 设置武器实体
	/// </summary>
	/// <param name="entity"></param>
	public void SetWeaponEntity(WeaponEntity entity)
    {
		Entity = entity;
	}

	public void SetEntityId(int id)
    {
		EntityId = id;
    }

	/// <summary>
	/// 子弹数量改变
	/// </summary>
	protected bool ChangeMagazineNum(int defaultMainMagazine)
    {
		if(MainMagazine > 0)
        {
			MainMagazine--;
			return true;
		}
		else
		{
			if(SpareMagazine > 0)
            {
				SpareMagazine -= defaultMainMagazine;
				MainMagazine += defaultMainMagazine;
				return true;
			}
			else
            {
				Debug.Log("子弹全部打完了！");
				return false;
            }
		}
	}

	/// <summary>
	/// 攻击
	/// </summary>
	public abstract void Attack();

	public virtual void OnUpdate() { }
}
