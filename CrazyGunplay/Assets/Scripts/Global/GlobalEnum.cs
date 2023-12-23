using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		全局枚举
*/

/// <summary>
/// 玩家控制器类型
/// </summary>
public enum ControllerType
{
	None,
	Move,	//移动
	Jump,	//跳跃
	Dush,	//突进
	NomralAttack,	//普攻
	UseSkill,	//技能
}

/// <summary>
/// 武器类型
/// </summary>
public enum WeaponType
{
	Gun,	//枪械
	Throw,	//投掷物
	NearRange,	//近战
}

/// <summary>
/// 子弹类型
/// </summary>
public enum BulletType
{
	/// <summary>
	/// 一般子弹
	/// </summary>
	Normal,

	/// <summary>
	/// 手雷，抛物线，范围爆炸
	/// </summary>
	Grenade,

	/// <summary>
	/// RPG，范围爆炸
	/// </summary>
	RPG,
}