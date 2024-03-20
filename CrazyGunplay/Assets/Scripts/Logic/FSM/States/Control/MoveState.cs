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
public class MoveState : PlayerState
{
    public override StateType Type => StateType.Move;

    public override bool OnExecute(PlayerEntity owner)
    {
        if(owner.Controller.GetJump())
        {
            owner.Machine.ChangeState(StateLayer.Control, StateType.Jump);
            return true;
        }

        if(!owner.Controller.GetMove())
        {
            Debug.Log("状态 IdleState!!");
            owner.Machine.ChangeState(StateLayer.Control, StateType.ControlIdle);
            return true;
        }

        return false;
    }
}
