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

	public WeaponEntity Entity { get; private set; }
	public Config_Weapon Config { get; private set; }

	public Weapon(Config_Weapon config, int playerId, int id)
    {
		PlayerId = playerId;
		Id = id;
		Config = config;
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
