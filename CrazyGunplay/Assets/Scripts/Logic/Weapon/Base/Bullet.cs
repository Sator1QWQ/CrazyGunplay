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
	/// 子弹数量
	/// </summary>
	public int BulletCount { get; private set; }

	public float FlySpeed { get; private set; }

	/// <summary>
	/// 子弹实体
	/// </summary>
	public List<BulletEntity> BulletEntityList { get; private set; }

	public int GunId { get; private set; }

	/// <summary>
	/// 外部不允许new，只能由BulletComponent new
	/// </summary>
	/// <param name="bulletId"></param>
	public Bullet(int bulletId, int gunId)
    {
		Id = bulletId;
		GunId = gunId;
		BulletCount = Config.Get<int>("Bullet", bulletId, "bulletCount");
		FlySpeed = Config.Get<float>("Bullet", bulletId, "flySpeed");
		BulletEntityList = new List<BulletEntity>();
    }

	/// <summary>
	/// 初始化数据，只能由BuleltComponent调用，外部不允许调用
	/// </summary>
	/// <param name="startPos"></param>
	/// <param name="startDirection"></param>
	public void InitBullet(Vector3 startPos, Vector3 startDirection)
    {
		StartPos = startPos;
		StartDirection = startDirection;
		BulletEntityList.Clear();
	}

	public void OnShowBullet()
    {
		//暂时这样写，后续喷子需要改
		for(int i = 0; i < BulletEntityList.Count; i++)
        {
			BulletEntityList[i].transform.position = StartPos;
			BulletEntityList[i].transform.rotation = Quaternion.Euler(StartDirection);
		}
    }

	public void AddBulletEntity(BulletEntity entity)
    {
		BulletEntityList.Add(entity);
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
