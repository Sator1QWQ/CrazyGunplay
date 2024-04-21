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
	TeamDead = 4,    //队伍全灭
	Timeout = 5,    //战斗倒计时结束
}

/// <summary>
/// 受击类型
/// </summary>
public enum GetHitType
{
	BeatBack = 1,	//击退
	HitToFly = 2,	//击飞
	No = 3,	//无效果
}

/// <summary>
/// 手雷的爆炸范围类型
/// </summary>
public enum GrenadeRangeType
{
	Circle = 1,	//圆形
	GroundCircle = 2,	//在地面上 圆形
}

/// <summary>
/// 子弹隐藏类型
/// </summary>
public enum BulletHideType
{
	HitPlayer = 1,	//击中玩家隐藏
	HitWall = 2,	//击中墙壁或者地板
	Custom = 4,	//程序自定义
}

/// <summary>
/// 技能类型
/// </summary>
public enum SkillType
{
	zhudong = 1,
	beidong = 2,
}

/// <summary>
/// 技能施法模式
/// </summary>
public enum SkillCastMode
{

}

/// <summary>
/// 技能目标类型
/// </summary>
public enum SkillTargetType
{

}

/// <summary>
/// buff类型
/// </summary>
public enum BuffType
{
	/// <summary>
	/// 移速修改
	/// </summary>
	MoveSpeed = 1,

	/// <summary>
	/// 受伤时的击退效果修改
	/// </summary>
	GetBeatBack = 2,

	/// <summary>
	/// 血量修改
	/// </summary>
	Hp = 3,

	/// <summary>
	/// 射速修改
	/// </summary>
	FireRate = 4,

	/// <summary>
	/// 攻击力修改
	/// </summary>
	BeatBackValue = 5,
}
