using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
	子弹类 跟随子弹实体一起生成
*/
public abstract class Bullet
{
	/// <summary>
	/// 配置表的id，场景中是会存在多个相同id的子弹的
	/// </summary>
	public int Id { get; private set; }

	/// <summary>
	/// 发射子弹时的坐标
	/// </summary>
	public Vector3 StartPos { get; private set; }

	/// <summary>
	/// 刚发射时方向
	/// </summary>
	public Vector3 StartDirection { get; private set; }  

	/// <summary>
	/// 当前坐标
	/// </summary>
	public Vector3 CurPos { get; protected set; }

	/// <summary>
	/// 当前方向
	/// </summary>
	public Vector3 CurDirection { get; protected set; }

	/// <summary>
	/// 子弹数量
	/// </summary>
	public int BulletCount { get; private set; }

	/// <summary>
	/// 子弹实体
	/// </summary>
	public BulletEntity BulletEntity { get; private set; }

	public Bullet(BulletEntity entity, int bulletId)
    {
		BulletEntity = entity;
		Id = bulletId;
		BulletCount = Config.Get<int>("Bulelt", bulletId, "bulletCount");
    }

	/// <summary>
	/// 开始飞行，只能由BuleltComponent调用，外部不允许调用
	/// </summary>
	/// <param name="startPos"></param>
	/// <param name="startDirection"></param>
	public void StartFly(Vector3 startPos, Vector3 startDirection)
    {
		StartPos = startPos;
		StartDirection = startDirection;
    }

	/// <summary>
	/// 子弹飞行时每帧调用，子类自行实现飞行轨迹
	/// </summary>
	public abstract void Fly();

	/// <summary>
	/// 子弹碰撞时调用
	/// </summary>
	public abstract void OnCollision(GameObject target);
}
