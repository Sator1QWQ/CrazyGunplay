using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;
using System;

/*
* 作者：
* 日期：
* 描述：
		枪械基类
*/
public class GunWeapon : Weapon
{
	/// <summary>
	/// 主弹夹
	/// </summary>
	public int MainMag { get; private set; }

	/// <summary>
	/// 备用弹夹
	/// </summary>
	public int SpareMag { get; private set; }

	/// <summary>
	/// 是否正在换弹
	/// </summary>
	public bool IsReloading { get; private set; }

	public Config_Gun GunConfig { get; private set; }

	private float rateTemp;
	private bool canAttack;
	private float reloadTemp;
	private bool reloadState;
	private bool isFireCooling;

	/// <summary>
	/// 外部不允许直接new这个对象
	/// </summary>
	/// <param name="configName"></param>
	/// <param name="id"></param>
	public GunWeapon(Config_Weapon config, int playerId, int id, PlayerEntity entity) : base(config, playerId, id, entity)
    {
		GunConfig = Config<Config_Gun>.Get("Gun", id);
		MainMag = GunConfig.mainMag;
		SpareMag = GunConfig.spareMag;

		canAttack = true;
	}

    public override void OnUpdate()
    {
		if(rateTemp >= GunConfig.fireRate)
        {
			isFireCooling = false;
			rateTemp = 0;
        }
		rateTemp += Time.deltaTime;

		if(reloadState)
        {
			if (reloadTemp >= GunConfig.reloadTime)
			{
				ReloadSuccess();
				IsReloading = false;
				reloadTemp = 0;
			}
			reloadTemp += Time.deltaTime;
		}
    }

    public override void Attack()
    {
		if(!CanAttack())
        {
			return;
        }

		int bulletId = Config<Config_Gun>.Get("Gun", Id).bulletId;
		Module.Bullet.ShowBullet(this, bulletId, Id, PlayerEntity.WeaponRoot.position, PlayerEntity.LookDirection);
		MainMag--;
		if(MainMag == 0)
        {
			if(SpareMag > 0)
            {
				StartReload();
			}
			
        }
		isFireCooling = true;
	}

	public bool CanReload()
    {
		int reloadCount = GunConfig.mainMag - MainMag;
		int addCount = Mathf.Min(reloadCount, SpareMag);
		return addCount > 0;
	}

	public bool CanAttack()
    {
		if (isFireCooling)
        {
			return false;
        }

		if(MainMag == 0 && SpareMag == 0)
        {
			return false;
        }

		if(IsReloading)
        {
			return false;
        }

		return true;
    }

	/// <summary>
	/// 换弹
	/// </summary>
	public void StartReload()
    {
		if(!CanReload())
        {
			return;
        }
		Debug.Log("换子弹！");
		reloadState = true;
		reloadTemp = 0;
		IsReloading = true;
	}

	public void StopReload()
    {
		if(!reloadState)
        {
			return;
        }

		Debug.Log("取消换弹");
		reloadTemp = 0;
		reloadState = false;
		IsReloading = false;
	}

	private void ReloadSuccess()
    {
		int reloadCount = GunConfig.mainMag - MainMag;
		int addCount = Mathf.Min(reloadCount, SpareMag);
		SpareMag -= addCount;
		MainMag += addCount;
		Debug.Log("换弹结束！");
		StopReload();
	}
}