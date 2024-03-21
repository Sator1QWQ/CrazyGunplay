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
public class PassiveIdleState : PlayerState
{
    public override StateType Type => StateType.PassiveIdle;

    public override bool OnExecute(PlayerEntity owner)
    {
        if (owner.Data.Life <= 0)
        {
            Debug.Log("转变状态为死亡");
            owner.Machine.ChangeState(Layer, StateType.Die);
            return true;
        }
        return false;
    }
}
