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
    public NearRangeWeapon(Config_Weapon config, int playerId, int id, PlayerEntity entity) : base(config, playerId, id, entity)
    {
    }

    public override void Attack()
    {
        Debug.Log("近战攻击未实现");
    }
}
