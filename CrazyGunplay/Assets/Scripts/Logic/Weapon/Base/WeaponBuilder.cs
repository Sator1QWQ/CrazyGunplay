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
		
*/
public class WeaponBuilder
{
	public static GunBase CreateWeapon(string configName, int id)
    {
		LuaTable table = Module.Lua.Env.Global.Get<LuaTable>(configName);
		LuaTable config = table.Get<int, LuaTable>(id);
		GunBase weapon = null;

		GunType type = config.Get<GunType>("gunType");
		switch (type)
		{
			case GunType.LongRange:
				weapon = new LongRangeGun(id, config);
				break;
			case GunType.Throw:
				weapon = new ThrowGun(id, config);
				break;
		}

		return weapon;
	}
}
