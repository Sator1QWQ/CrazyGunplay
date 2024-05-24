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
	/// 使用的武器
	/// </summary>
	public Weapon OwnerWeapon { get; private set; }

	public bool IsFirstFly { get; set; }

	public bool CanHitSelf { get; private set; }
	public BulletHideType HideType { get; private set; }

	/// <summary>
	/// 外部不允许new，只能由BulletComponent new
	/// </summary>
	/// <param name="bulletId"></param>
	public Bullet(Weapon ownerWeapon, int bulletId, int gunId)
    {
		OwnerWeapon = ownerWeapon;
		Id = bulletId;
		GunId = gunId;
		Config_Bullet config = Config<Config_Bullet>.Get("Bullet", bulletId);
		BulletCount = config.bulletCount;
		FlySpeed = config.flySpeed;
		CanHitSelf = config.canHitSelf;
		HideType = config.bulletHideType;
		BulletEntityList = new List<BulletEntity>();
		IsFirstFly = true;
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
			BulletEntityList[i].transform.rotation = Quaternion.LookRotation(StartDirection);
			BulletEntityList[i].transform.right = BulletEntityList[i].transform.forward;	//right看向原来的forward
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

	public virtual void FirstFly() { }

	/// <summary>
	/// 子弹碰撞到其他障碍物时时调用
	/// </summary>
	/// <param name="target"></param>
	/// <returns>是否需要隐藏子弹</returns>
	public virtual void OnHitWall(GameObject target)
    {
		
    }

	/// <summary>
	/// 子弹击中玩家时调用
	/// </summary>
	/// <returns>是否可以触发伤害</returns>
	public virtual bool OnHitPlayer(PlayerEntity entity) => true;

	/// <summary>
	/// 是否需要隐藏子弹
	/// </summary>
	/// <returns></returns>
	public virtual bool CustomCheckHide() => false;

	public abstract void DoAttackAction(PlayerEntity playerEntity);

	/// <summary>
	/// 发送攻击事件，增加击退值
	/// </summary>
	/// <param name="playerId"></param>
	protected void SendAttackEvent(int playerId)
    {
		HitData data = new HitData();
		data.dealerId = OwnerWeapon.PlayerId;
		data.receiverId = playerId;
		data.buffList = new List<BuffValue>();
		data.hitType = GetHitType.BeatBack;
		data.buffList.Add(new BuffValue() { buffId = 110, duration = 0, value = OwnerWeapon.Config.beatBack });
		HitEventArgs args = HitEventArgs.Create(data);
		Module.Event.FireNow(this, args);
	}
}
