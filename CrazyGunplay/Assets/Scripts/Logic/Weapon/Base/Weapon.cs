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
	public int Id { get; private set; }
	public WeaponType Type { get; protected set; }

	public WeaponEntity Entity { get; private set; }

	public Weapon(WeaponEntity entity, LuaTable config, int id)
    {
		Id = id;
		Entity = entity;
		Type = config.Get<WeaponType>("weaponType");
    }

	/// <summary>
	/// 攻击
	/// </summary>
	public abstract void Attack();

	public virtual void OnUpdate() { }
}
