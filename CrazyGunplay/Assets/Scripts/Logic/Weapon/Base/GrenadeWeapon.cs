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
		手雷
*/
public class GrenadeWeapon : Weapon
{
    public int MainMag { get; private set; }

    private Config_Grenade config;

    private float rateTemp;
    private bool isFireCooling;

    public GrenadeWeapon(Config_Weapon weaponConfig, int playerId, int id, PlayerEntity entity) : base(weaponConfig, playerId, id, entity)
    {
        config = Config<Config_Grenade>.Get("Grenade", id);
        MainMag = config.mainMag;
    }

    public override void OnUpdate()
    {
        if (rateTemp >= config.fireRate + config.throwTime)
        {
            isFireCooling = false;
            rateTemp = 0;
        }
        rateTemp += Time.deltaTime;
    }

    public override void Attack()
    {
        //延迟一段时间 注意定时器的销毁
        Module.Timer.AddTimer(data =>
        {
            Debug.Log("投掷物攻击！");
            Config_Grenade cfg = Config<Config_Grenade>.Get("Grenade", Id);
            if (MainMag > 0)
            {
                int bulletId = cfg.bulletId;
                Module.Bullet.ShowBullet(this, bulletId, Id, PlayerEntity.WeaponRoot.position, PlayerEntity.LookDirection);
            }
            MainMag--;
        }, config.throwTime);
        isFireCooling = true;
    }

    public override bool CanAttack()
    {
        if(isFireCooling)
        {
            return false;
        }

        return true;
    }
}
