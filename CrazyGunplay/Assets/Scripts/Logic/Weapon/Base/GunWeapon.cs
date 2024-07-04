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
		//子弹一开始为0
		if (MainMag == 0)
		{
			if (SpareMag > 0)
			{
				StartReload();
			}
			else
            {
				Module.Audio.PlayAudio(PlayerEntity.AudioSource, GlobalDefine.GUN_AUDIO_PATH + "EmptyBullet.mp3");
            }
			return;
		}

		int bulletId = Config<Config_Gun>.Get("Gun", Id).bulletId;
		Module.Bullet.ShowBullet(this, bulletId, Id, PlayerEntity.WeaponRoot.position, PlayerEntity.LookDirection);
		MainMag--;
		isFireCooling = true;
		string path = GlobalDefine.GUN_AUDIO_PATH + GunConfig.directoryName + "/Shoot.mp3";
		Module.Audio.PlayAudio(Entity.PlayerEntity.AudioSource, path);
		BulletCountChangeEventArgs args = BulletCountChangeEventArgs.Create(PlayerEntity.PlayerId);
		Module.Event.FireNow(this, args);

		if (MainMag == 0)
		{
			if (SpareMag > 0)
			{
				StartReload();
			}
		}
	}

	public bool CanReload()
    {
		int reloadCount = GunConfig.mainMag - MainMag;
		int addCount = Mathf.Min(reloadCount, SpareMag);
		return addCount > 0;
	}

	public override bool CanAttack()
    {
		if (isFireCooling)
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

		string path = GlobalDefine.GUN_AUDIO_PATH + GunConfig.directoryName + "/Reload.mp3";
		Module.Audio.PlayAudio(Entity.PlayerEntity.AudioSource, path);
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
		WeaponReloadFinishEventArgs args = WeaponReloadFinishEventArgs.Create(PlayerEntity.PlayerId);
		Module.Event.FireNow(this, args);
	}
}