﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;

/*
* 作者：
* 日期：
* 描述：
		步枪，手枪子弹
*/
public class NormalBullet : Bullet
{
    private LuaTable weaponConfig;
    private int[] bulletOffset; //[0]最小角度 [1]最大角度
    private int[] randomAngle;

    public NormalBullet(Weapon ownerWeapon, int bulletId, int gunId) : base(ownerWeapon, bulletId, gunId)
    {
        weaponConfig = Config.Get("Weapon", gunId);
        bulletOffset = Config.Get<int[]>("Bullet", bulletId, "bulletOffset");
        Init();
    }

    private void Init()
    {
        randomAngle = new int[BulletCount];
        for (int i = 0; i < BulletCount; i++)
        {
            randomAngle[i] = Random.Range(bulletOffset[0], bulletOffset[1]);
        }
    }

    public override void Fly()
    {
        for(int i = 0; i < BulletEntityList.Count; i++)
        {
            //Debug.Log("随机角度为==" + randomAngle[i]);
            Vector3 newDire = Quaternion.Euler(0, 0, randomAngle[i]) * StartDirection;
            BulletEntityList[i].Gravity.AddVelocity("bullet", newDire.normalized * FlySpeed, -1, false);
        }
    }

    public override void DoAttackAction(PlayerEntity player)
    {
        if (player == null)
        {
            return;
        }
        SendAttackEvent(player.PlayerId);
        GetHitType type = (GetHitType)weaponConfig.Get<int>("beatType");
        player.GetDamage(type, StartDirection.normalized);
    }
}
