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
	public Weapon Weapon { get; private set; }

	/// <summary>
	/// 设置武器对象
	/// </summary>
	/// <param name="weapon"></param>
	public void SetWeapon(Weapon weapon)
    {
		Weapon = weapon;
    }
}
