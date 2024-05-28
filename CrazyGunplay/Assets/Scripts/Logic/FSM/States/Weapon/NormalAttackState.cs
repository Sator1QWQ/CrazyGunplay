using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class NormalAttackState : PlayerState
{
    public override StateType Type => StateType.NormalAttack;
    private Weapon weapon;

    public override void OnEnter(PlayerEntity owner)
    {
        weapon = owner.WeaponManager.CurrentWeapon;
    }

    public override void OnExit(PlayerEntity owner)
    {
        weapon = null;
    }

    public override bool OnExecute(PlayerEntity owner)
    {
        Debug.Log("攻击状态");
        if(!owner.Controller.GetNormalAttack())
        {
            owner.Machine.ChangeState(Layer, StateType.WeaponIdle);
            return true;
        }

        GunWeapon gun = weapon as GunWeapon;
        if (gun != null && gun.CanReload() && (owner.Controller.GetWeaponReload() || gun.IsReloading))
        {
            owner.Machine.ChangeState(Layer, StateType.Reload);
            return true;
        }

        return false;
    }
}
