using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using GameFramework.Event;

/*
* 作者：
* 日期：
* 描述：
	死亡状态	
*/
public class DieState : PlayerState
{
    public override StateType Type => StateType.Die;

    public override void OnEnter(PlayerEntity owner)
    {
        Debug.Log("玩家" + owner.PlayerId + "死亡");
        PlayerDieEventArgs args = PlayerDieEventArgs.Create(owner.PlayerId, owner.Data.Life);
        Module.Event.FireNow(this, args);
    }

    public override bool OnExecute(PlayerEntity owner)
    {
        //状态不为结束状态
        if(Module.PlayerData.State != BattleState.End && Module.PlayerData.State != BattleState.None)
        {
            owner.Machine.ChangeState(StateLayer.Passive, StateType.Respawn);
            return true;
        }

        return false;
    }
}
