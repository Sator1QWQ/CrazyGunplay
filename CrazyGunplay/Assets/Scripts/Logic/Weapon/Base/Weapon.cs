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
	public int PlayerId { get; private set; }
	public int Id { get; private set; }
	public WeaponType Type { get; private set; }
	public int TargetId { get; private set; }

	public WeaponEntity Entity { get; private set; }

	public Weapon(LuaTable config, int playerId, int id)
    {
		PlayerId = playerId;
		Id = id;
		Type = config.Get<WeaponType>("weaponType");
		TargetId = config.Get<int>("targetId");
    }

	/// <summary>
	/// 初始化武器实体
	/// </summary>
	/// <param name="entity"></param>
	public void InitWeaponEntity(WeaponEntity entity)
    {
		Entity = entity;
	}

	/// <summary>
	/// 攻击
	/// </summary>
	public abstract void Attack();

	public virtual void OnUpdate() { }
}
