using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;

/*
* 作者：
* 日期：
* 描述：
		武器系统
*/
public class WeaponComponent : GameFrameworkComponent
{
	private List<Weapon> weaponList = new List<Weapon>();

	/// <summary>
	/// 所有武器都从这生成
	/// </summary>
	/// <param name="entity"></param>
	/// <param name="configName"></param>
	/// <param name="id"></param>
	/// <returns></returns>
	public Weapon CreateWeapon(WeaponEntity entity, string configName, int id)
	{
		LuaTable table = Module.Lua.Env.Global.Get<LuaTable>(configName);
		LuaTable config = table.Get<int, LuaTable>(id);
		Weapon weapon = null;

		WeaponType type = config.Get<WeaponType>("weaponType");
		switch (type)
		{
			case WeaponType.Gun:
				weapon = new GunWeapon(entity, config, id);
				break;
			case WeaponType.Throw:
				weapon = new ThrowWeapon(entity, config, id);
				break;
			case WeaponType.NearRange:
				weapon = new NearRangeWeapon(entity, config, id);
				break;
		}

		weaponList.Add(weapon);
		return weapon;
	}
}
