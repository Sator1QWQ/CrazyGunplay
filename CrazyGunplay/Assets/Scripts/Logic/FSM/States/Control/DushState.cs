using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DushState : PlayerState
{
    public override StateType Type => StateType.Dush;
    private bool isEnd = false;

    public override void OnEnter(PlayerEntity owner)
    {
        Module.Timer.AddTimer(date =>
        {
            isEnd = true;
        }, GlobalDefine.DUSH_TIME + 0.1f);  //+0.1f是为了保持动画完整
    }

    public override void OnExit(PlayerEntity owner)
    {
        isEnd = false;
    }

    public override bool OnExecute(PlayerEntity owner)
    {
        if(isEnd)
        {
            owner.Machine.ChangeState(StateLayer.Control, StateType.ControlIdle);
            return true;
        }

        return false;
    }
}
