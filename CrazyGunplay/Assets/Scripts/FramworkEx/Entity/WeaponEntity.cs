using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		武器实体
*/
public class WeaponEntity : EntityLogic
{
	/// <summary>
	/// 武器持有者
	/// </summary>
	public PlayerEntity PlayerEntity { get; private set; }

	public void SetPlayerEntity(PlayerEntity entity)
    {
		PlayerEntity = entity;
    }
}
