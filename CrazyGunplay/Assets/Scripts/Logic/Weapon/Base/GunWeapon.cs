using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;

/*
* 作者：
* 日期：
* 描述：
		枪械基类
*/
public class GunWeapon : Weapon
{

	/// <summary>
	/// 射速
	/// </summary>
	public float FireRate { get; private set; }
	
	/// <summary>
	/// 射程
	/// </summary>
	public float Range { get; private set; }

	/// <summary>
	/// 换弹时间
	/// </summary>
	public float ReloadTime { get; private set; }

	/// <summary>
	/// 主弹夹
	/// </summary>
	public float MainMag { get; private set; }

	/// <summary>
	/// 备用弹夹
	/// </summary>
	public float SpareMag { get; private set; }

	/// <summary>
	/// 每发子弹产生后坐力
	/// </summary>
	public float PerRecoil { get; private set; }

	/// <summary>
	/// 最大后坐力
	/// </summary>
	public float MaxRecoil { get; private set; }

	private float rateTemp;
	private bool canAttack;

	/// <summary>
	/// 外部不允许直接new这个对象
	/// </summary>
	/// <param name="configName"></param>
	/// <param name="id"></param>
	public GunWeapon(Config_Weapon config, int playerId, int id, PlayerEntity entity) : base(config, playerId, id, entity)
    {
		Config_Gun cfg = Config<Config_Gun>.Get("Gun", id);
		FireRate = cfg.fireRate;
		Range = cfg.range;
		ReloadTime = cfg.reloadTime;
		MainMag = cfg.mainMag;
		SpareMag = cfg.spareMag;

		canAttack = true;
	}

    public override void OnUpdate()
    {
		if(rateTemp >= FireRate)
        {
			canAttack = true;
			rateTemp = 0;
        }
		rateTemp += Time.deltaTime;
    }

    public override void Attack()
    {
		if(!canAttack)
        {
			return;
        }
		int bulletId = Config<Config_Gun>.Get("Gun", Id).bulletId;
		Module.Bullet.ShowBullet(this, bulletId, Id, PlayerEntity.WeaponRoot.position, PlayerEntity.LookDirection);
		canAttack = false;
    }

	/// <summary>
	/// 换弹
	/// </summary>
	public void Reload()
    {
		Debug.Log("换子弹！");
    }
}