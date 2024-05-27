using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		被击飞
*/
public class GetHitFlyState : PlayerState
{
    public override StateType Type => StateType.GetHitFly;

    public override bool OnExecute(PlayerEntity owner)
    {
        Debug.Log("正在被击飞");
        if(!owner.Controller.Gravity.IsAir)
        {
            Debug.Log("击飞状态结束");
            owner.Machine.ChangeState(Layer, StateType.PassiveIdle);
            return true;
        }
        return false;
    }
}
