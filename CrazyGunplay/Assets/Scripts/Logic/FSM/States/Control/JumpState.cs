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
        if(!owner.Controller.GetJump())
        {
            owner.Machine.ChangeState(StateLayer.Control, StateType.ControlIdle);
            return true;
        }

        return false;
    }
}
