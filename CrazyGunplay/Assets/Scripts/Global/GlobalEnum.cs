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
	ChangeWeapon,	//切换武器
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
/// 技能行为
/// </summary>
public enum SkillCastAction
{
	/// <summary>
	/// 无行为
	/// </summary>
	None = 1,

	/// <summary>
	/// 获取武器
	/// </summary>
	GetWeapon = 2,

	/// <summary>
	/// 召唤物
	/// </summary>
	Summon = 3,

	/// <summary>
	/// 位移
	/// </summary>
	Move = 4,

	/// <summary>
	/// Buff
	/// </summary>
	Buff = 5,

	/// <summary>
	/// 伤害
	/// </summary>
	Damage = 6,

	/// <summary>
	/// 击飞
	/// </summary>
	Beatback = 7,
}

/// <summary>
/// 技能激活条件
/// </summary>
public enum SkillActiveConditionType
{
	/// <summary>
	/// 根据时间激活
	/// </summary>
	Time = 1,

	/// <summary>
	/// 伤害量
	/// </summary>
	Damage = 2,

	/// <summary>
	/// 受伤量，beatBackValue
	/// </summary>
	GetHit = 3,
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

/// <summary>
/// 目标类型
/// </summary>
public enum TargetType
{
	/// <summary>
	/// 无目标
	/// </summary>
	NoTarget = 1,

	/// <summary>
	/// 玩家自己
	/// </summary>
	Self = 2,

	/// <summary>
	/// 友方
	/// </summary>
	SelfTeam = 3,

	/// <summary>
	/// 敌方
	/// </summary>
	EnemyTeam = 4,

	/// <summary>
	/// 所有玩家
	/// </summary>
	AllTeam = 5,
}

/// <summary>
/// 目标选择模式
/// </summary>
public enum TargetSelectMode
{
	/// <summary>
	/// 距离
	/// </summary>
	Distance = 1,

	/// <summary>
	/// 血量，BeatbackValue 血量越少beatBackValue越大
	/// </summary>
	HP = 2,

	/// <summary>
	/// 无
	/// </summary>
	None = 3,
}

/// <summary>
/// 比较的类型
/// </summary>
public enum CompareType
{
	/// <summary>
	/// 大于
	/// </summary>
	Greater = 1,

	/// <summary>
	/// 小于
	/// </summary>
	Less = 2,

	/// <summary>
	/// 等于
	/// </summary>
	Equal = 3,

	/// <summary>
	/// 最小
	/// </summary>
	Min = 4,

	/// <summary>
	/// 最大
	/// </summary>
	Max = 5,

	/// <summary>
	/// 无
	/// </summary>
	None = 6,
}

public enum SkillState
{
	/// <summary>
	/// 施法前摇
	/// </summary>
	BeforeCast,

	/// <summary>
	/// 使用技能
	/// </summary>
	UseSkill,

	/// <summary>
	/// 施法后摇
	/// </summary>
	AfterCast,

	/// <summary>
	/// 技能结束
	/// </summary>
	End,
}

/// <summary>
/// 技能进入冷却的时机
/// </summary>
public enum SkillCoolingTiming
{
	/// <summary>
	/// 使用技能时
	/// </summary>
	WhenUseSkill = 1,

	/// <summary>
	/// 技能结束时
	/// </summary>
	WhenEndSkill = 2,
}

/// <summary>
/// 技能特效删除的时机
/// </summary>
public enum SkillEffectDeleteTiming
{
	/// <summary>
	/// 当特效结束时
	/// </summary>
	WhenEffectEnd = 1,

	/// <summary>
	/// 当技能结束时
	/// </summary>
	WhenSkillEnd = 2,
}

/// <summary>
/// 技能表现播放的时机
/// </summary>
public enum SkillExpressionPlayTiming
{
	/// <summary>
	/// 使用技能时
	/// </summary>
	WhenUseSkill = 1,

	/// <summary>
	/// 当命中时
	/// </summary>
	WhenHit = 2,
}

/// <summary>
/// 范围类型
/// </summary>
public enum AreaType
{
	/// <summary>
	/// 圆形
	/// </summary>
	Circle = 1,

	/// <summary>
	/// 直线型
	/// </summary>
	Line = 2,

	/// <summary>
	/// 矩形
	/// </summary>
	Rectangle = 3,

	/// <summary>
	/// 扇形
	/// </summary>
	fanshaped = 4,
}