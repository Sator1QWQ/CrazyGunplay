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
    public float FireRate { get; private set; }

    private float rateTemp;
    private bool canAttack;

    public GrenadeWeapon(LuaTable config, int playerId, int id) : base(config, playerId, id)
    {
         FireRate = Config.Get<float>("Grenade", id, "fireRate");
    }

    public override void OnUpdate()
    {
        if (rateTemp >= FireRate)
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
        int bulletId = Config.Get<int>("Grenade", Id, "bulletId");
        Module.Bullet.ShowBullet(this, bulletId, Id, Entity.PlayerEntity.WeaponRoot.position, Entity.PlayerEntity.LookDirection);
        canAttack = false;
    }
}
