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
		投掷物
*/
public class ThrowWeapon : Weapon
{
    public ThrowWeapon(LuaTable config, int id) : base(config, id)
    {
    }

    public override void Attack()
    {
        Debug.Log("投掷物攻击！");
    }
}
