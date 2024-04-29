using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;
using GameFramework.Event;
using System;

/*
* 作者：
* 日期：
* 描述：
		武器系统
*/
public class WeaponComponent : GameFrameworkComponent
{
	private void Start()
	{
		Module.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
		Debug.Log("添加实体组Weapon");
	}

	//show成功的话会发送事件
	private void OnShowEntity(object obj, GameEventArgs e)
    {
		ShowEntitySuccessEventArgs showEvent = e as ShowEntitySuccessEventArgs;
		if(!showEvent.EntityLogicType.Equals(typeof(WeaponEntity)))
        {
			return;
        }
		Weapon weapon = (Weapon)showEvent.UserData;
		weapon.SetWeaponEntity(showEvent.Entity.Logic as WeaponEntity);
	}

	/// <summary>
	/// 新生成武器
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="configName"></param>
	/// <param name="id"></param>
	/// <returns></returns>
	public Weapon NewWeapon(PlayerEntity playerEntity, int id)
	{
		Config_Weapon cfg = Config<Config_Weapon>.Get("Weapon", id);
		Weapon weapon = null;
		int playerId = playerEntity.PlayerId;

		WeaponType type = cfg.weaponType;
		switch (type)
		{
			case WeaponType.Gun:
				weapon = new GunWeapon(cfg, playerId, id, playerEntity);
				break;
			case WeaponType.Throw:
				weapon = new GrenadeWeapon(cfg, playerId, id, playerEntity);
				break;
			case WeaponType.NearRange:
				weapon = new NearRangeWeapon(cfg, playerId, id, playerEntity);
				break;
		}
		string path = cfg.path;
		int entId = EntityTool.GetWeaponEntityId();
		weapon.SetEntityId(entId);
		Debug.Log("show了一个武器 实体id==" + entId);
		Module.Entity.ShowEntity(entId, typeof(WeaponEntity), path, "Weapon", weapon);
		return weapon;
	}
}
