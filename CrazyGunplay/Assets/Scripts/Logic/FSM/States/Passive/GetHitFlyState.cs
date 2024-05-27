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
    private float tempTime = 0;
    public override StateType Type => StateType.GetHitFly;

    public override void OnEnter(PlayerEntity owner)
    {
        owner.SetCanMove(false);
    }

    public override void OnExit(PlayerEntity owner)
    {
        tempTime = 0;
        owner.SetCanMove(true);
    }

    public override bool OnExecute(PlayerEntity owner)
    {
        Debug.Log("正在被击飞");
        if(tempTime >= GlobalDefine.HIT_FLY_TIME)
        {
            tempTime = 0;
            Debug.Log("击飞状态结束");
            owner.Machine.ChangeState(Layer, StateType.PassiveIdle);
            return true;
        }
        tempTime += Time.deltaTime;
        return false;
    }
}
