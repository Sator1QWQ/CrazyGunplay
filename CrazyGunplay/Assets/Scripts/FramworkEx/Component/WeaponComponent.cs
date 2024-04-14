using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;
using GameFramework.Event;

/*
* 作者：
* 日期：
* 描述：
		武器系统
*/
public class WeaponComponent : GameFrameworkComponent
{
	//key:playerId
	private Dictionary<int, Weapon> weaponDic = new Dictionary<int, Weapon>();

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
		weapon.InitWeaponEntity(showEvent.Entity.Logic as WeaponEntity);
	}

	/// <summary>
	/// 所有武器都从这生成
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="configName"></param>
	/// <param name="id"></param>
	/// <returns></returns>
	public Weapon ShowWeapon(string configName, int playerId, int id)
	{
		LuaTable config = Config.Get(configName, id);
		Weapon weapon = null;

		WeaponType type = config.Get<WeaponType>("weaponType");
		switch (type)
		{
			case WeaponType.Gun:
				weapon = new GunWeapon(config, playerId, id);
				break;
			case WeaponType.Throw:
				weapon = new GrenadeWeapon(config, playerId, id);
				break;
			case WeaponType.NearRange:
				weapon = new NearRangeWeapon(config, playerId, id);
				break;
		}

		weaponDic.Add(playerId, weapon);
		string path = config.Get<string>("path");
		int entId = EntityTool.GetWeaponEntityId();
		Debug.Log("show了一个武器 实体id==" + entId);
		Module.Entity.ShowEntity(entId, typeof(WeaponEntity), path, "Weapon", weapon);
		return weapon;
	}

	/// <summary>
	/// 获取玩家身上的武器
	/// </summary>
	/// <param name="id">玩家id</param>
	/// <returns></returns>
	public Weapon GetWeapon(int playerId) => weaponDic[playerId];
}
