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
    public Config_Grenade Config { get; set; }

    private float rateTemp;
    private bool canAttack;

    public GrenadeWeapon(Config_Weapon config, int playerId, int id) : base(config, playerId, id)
    {
        Config = Config<Config_Grenade>.Get("Grenade", id);
    }

    public override void OnUpdate()
    {
        if (rateTemp >= Config.fireRate)
        {
            canAttack = true;
            rateTemp = 0;
        }
        rateTemp += Time.deltaTime;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        Debug.Log("投掷物攻击！");
        Config_Grenade cfg = Config<Config_Grenade>.Get("Grenade", Id);
        int bulletId = cfg.bulletId;
        Module.Bullet.ShowBullet(this, bulletId, Id, Entity.PlayerEntity.WeaponRoot.position, Entity.PlayerEntity.LookDirection);
        canAttack = false;
    }
}
