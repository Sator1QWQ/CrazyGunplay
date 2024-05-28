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

	public Weapon(Config_Weapon config, int playerId, int id, PlayerEntity playerEntity)
    {
		PlayerId = playerId;
		Id = id;
		Config = config;
		PlayerEntity = playerEntity;
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
	/// 攻击
	/// </summary>
	public abstract void Attack();

	/// <summary>
	/// 是否可攻击
	/// </summary>
	/// <returns></returns>
	public abstract bool CanAttack();

	public virtual void OnUpdate() { }
}
