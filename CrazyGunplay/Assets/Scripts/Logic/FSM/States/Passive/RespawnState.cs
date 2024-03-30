using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 重生状态
/// </summary>
public class RespawnState : PlayerState
{
    public override StateType Type => StateType.Respawn;

    public override void OnEnter(PlayerEntity owner)
    {
        //设置到复活点
        owner.Entity.transform.position = Vector3.up * 10;
        owner.Machine.ChangeState(StateLayer.Passive, StateType.PassiveIdle);
    }
}
