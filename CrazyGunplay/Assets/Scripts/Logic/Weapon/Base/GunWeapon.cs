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

	/// <summary>
	/// 子弹
	/// </summary>
	public Bullet Bullet { get; private set; }

	/// <summary>
	/// 外部不允许直接new这个对象
	/// </summary>
	/// <param name="configName"></param>
	/// <param name="id"></param>
	public GunWeapon(WeaponEntity entity, LuaTable config, int id) : base(entity, config, id)
    {
		FireRate = config.Get<float>("fireRate");
		Range = config.Get<float>("range");
		ReloadTime = config.Get<float>("reloadTime");
		MainMag = config.Get<float>("mainMag");
		SpareMag = config.Get<float>("spareMag");
	}

    public override void Attack()
    {
		Debug.Log("枪射击");
    }

	/// <summary>
	/// 换弹
	/// </summary>
	public void Reload()
    {
		Debug.Log("换子弹！");
    }
}