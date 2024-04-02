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
public class JumpState : PlayerState
{
    public override StateType Type => StateType.Jump;

    public override bool OnExecute(PlayerEntity owner)
    {
        if (owner.Controller.Gravity.vt.y == 0 && !owner.Controller.Gravity.IsAir)
        {
            owner.Machine.ChangeState(StateLayer.Control, StateType.ControlIdle);
            return true;
        }

        if(owner.Controller.GetJump())
        {
            owner.Machine.ChangeState(StateLayer.Control, StateType.Jump);
            owner.Anim.SetTrigger("getJump");
            return true;
        }

        if(owner.Controller.GetDush())
        {
            owner.Machine.ChangeState(StateLayer.Control, StateType.Dush);
            return true;
        }
        return false;
    }
}
