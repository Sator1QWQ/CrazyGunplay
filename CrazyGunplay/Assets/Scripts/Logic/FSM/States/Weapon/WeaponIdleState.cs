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
public class WeaponIdleState : PlayerState
{
    public override StateType Type => StateType.WeaponIdle;
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
        if(owner.Controller.IsPause)
        {
            return false;
        }

        if(!owner.Controller.IsActionPause(ControllerType.NomralAttack) && owner.Controller.GetNormalAttack())
        {
            owner.Machine.ChangeState(Layer, StateType.NormalAttack);
            return true;
        }

        GunWeapon gun = weapon as GunWeapon;
        if(gun != null && gun.CanReload() && (owner.Controller.GetWeaponReload() || gun.IsReloading))
        {
            owner.Machine.ChangeState(Layer, StateType.Reload);
            return true;
        }

        return false;
    }
}
