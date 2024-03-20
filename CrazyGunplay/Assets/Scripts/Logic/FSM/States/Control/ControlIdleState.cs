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
public class ControlIdleState : PlayerState
{
    public override StateType Type => StateType.ControlIdle;

    public override bool OnExecute(PlayerEntity owner)
    {
        if(owner.Controller.GetMove())
        {
            owner.Machine.ChangeState(StateLayer.Control, StateType.Move);
            return true;
        }

        if(owner.Controller.GetJump())
        {
            owner.Machine.ChangeState(StateLayer.Control, StateType.Jump);
            return true;
        }

        return false;
    }
}
