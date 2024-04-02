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
	Gun = 1,	//枪械
	Throw = 2,	//投掷物
	NearRange = 3,	//近战
}

/// <summary>
/// 子弹类型
/// </summary>
public enum BulletType
{
	/// <summary>
	/// 一般子弹
	/// </summary>
	Normal = 1,

	/// <summary>
	/// 手雷，抛物线，范围爆炸
	/// </summary>
	Grenade = 2,

	/// <summary>
	/// RPG，范围爆炸
	/// </summary>
	RPG = 3,
}

/// <summary>
/// 角色状态类型
/// </summary>
public enum StateType
{
	//控制层
	ControlIdle,   //控制层闲置
	Move,   //移动
	Jump,   //跳跃
	Dush,   //突进

	//被动层
	PassiveIdle,	//被动层闲置
	GetHitFly,  //被击飞
	GetHit, //被击退
	Die,    //死亡
	Respawn,	//重生

	//技能层
	SkillIdle,	//技能层闲置
	UseSkill,   //使用技能

	//武器层
	WeaponIdle,	//武器层闲置
	NormalAttack,   //普攻
	Reload, //换弹

	None = -1,
}

/// <summary>
/// 状态机层级类型
/// </summary>
public enum StateLayer
{
	Control,	//控制层
	Passive,	//被动层
	Skill,	//技能层
	Weapon,	//武器层

	None = -1,
}

//战斗状态
public enum BattleState
{
	None = 1,
	Ready = 2,  //准备阶段
	Battle = 3, //战斗阶段
	End = 4,    //结束阶段
}
