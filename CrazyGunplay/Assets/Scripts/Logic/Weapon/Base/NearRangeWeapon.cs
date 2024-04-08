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
		近战武器(暂时不做)
*/
public class NearRangeWeapon : Weapon
{
    public NearRangeWeapon(LuaTable config, int playerId, int id) : base(config, playerId, id)
    {
    }

    public override void Attack()
    {
        Debug.Log("近战攻击未实现");
    }
}
