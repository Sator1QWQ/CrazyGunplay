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

    public override bool OnExecute(PlayerEntity owner)
    {
        if(owner.Controller.GetNormalAttack())
        {
            owner.Machine.ChangeState(Layer, StateType.NormalAttack);
            return true;
        }


        return false;
    }
}
