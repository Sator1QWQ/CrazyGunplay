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
public class ReloadState : PlayerState
{
    public override StateType Type => StateType.Reload;

    private bool isChange;
    private GunWeapon currentGun;

    public override void OnEnter(PlayerEntity owner)
    {
        owner.WeaponManager.OnChangeSlot += OnChangeSlot;
        currentGun = owner.WeaponManager.CurrentWeapon as GunWeapon;
        currentGun.StartReload();
        owner.Controller.SetPauseControl(ControllerType.NomralAttack, true);   //换弹期间不可射击
    }

    public override void OnExit(PlayerEntity owner)
    {
        isChange = false;
        currentGun = null;
        owner.WeaponManager.OnChangeSlot -= OnChangeSlot;
        owner.Controller.SetPauseControl(ControllerType.NomralAttack, false);
    }

    public override bool OnExecute(PlayerEntity owner)
    {
        //切换武器，则取消换弹
        if(isChange)
        {
            currentGun.StopReload();
            owner.Machine.ChangeState(Layer, StateType.WeaponIdle);
            return true;
        }

        if(!currentGun.IsReloading)
        {
            owner.Machine.ChangeState(Layer, StateType.WeaponIdle);
            return true;
        }

        return false;
    }

    private void OnChangeSlot(int lastWeaponId, int currentWeaponId)
    {
        isChange = true;
    }
}
