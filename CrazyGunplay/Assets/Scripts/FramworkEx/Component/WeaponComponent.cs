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
	private List<Weapon> weaponList = new List<Weapon>();

	private void Start()
	{
		Module.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
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
	public Weapon ShowWeapon(string configName, int id)
	{
		LuaTable config = Config.Get(configName, id);
		Weapon weapon = null;

		WeaponType type = config.Get<WeaponType>("weaponType");
		switch (type)
		{
			case WeaponType.Gun:
				weapon = new GunWeapon(config, id);
				break;
			case WeaponType.Throw:
				weapon = new ThrowWeapon(config, id);
				break;
			case WeaponType.NearRange:
				weapon = new NearRangeWeapon(config, id);
				break;
		}

		weaponList.Add(weapon);
		string path = config.Get<string>("path");
		Module.Entity.ShowEntity(EntityTool.GetWeaponEntityId(), typeof(WeaponEntity), path, "normal", weapon);
		return weapon;
	}

	/// <summary>
	/// 获取武器
	/// </summary>
	/// <param name="id">武器id</param>
	/// <returns></returns>
	public Weapon GetWeapon(int id) => weaponList.Find(weapon => weapon.Id == id);
}
