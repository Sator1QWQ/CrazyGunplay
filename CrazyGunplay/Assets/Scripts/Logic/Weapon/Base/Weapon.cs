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
		武器类
*/
public abstract class Weapon
{
	public int Id { get; protected set; }
	public WeaponEntity Entity { get; private set; }

	public Weapon(WeaponEntity entity, LuaTable config, int id)
    {
		Entity = entity;
    }

	/// <summary>
	/// 攻击
	/// </summary>
	public abstract void Attack();
}
