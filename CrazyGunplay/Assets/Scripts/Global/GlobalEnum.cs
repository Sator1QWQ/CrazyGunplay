﻿using System.Collections;
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
/// 枪械类型
/// </summary>
public enum GunType
{
	LongRange,	//远程
	Throw,	//投掷
}