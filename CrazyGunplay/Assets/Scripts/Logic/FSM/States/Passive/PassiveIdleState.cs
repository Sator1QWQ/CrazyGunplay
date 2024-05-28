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
		
*/
public class PassiveIdleState : PlayerState
{
    public override StateType Type => StateType.PassiveIdle;
    private bool isBeatFly;
    private int playerId;

    public override void OnEnter(PlayerEntity owner)
    {
        Module.Event.Subscribe(PlayerBeatFlyEventArgs.EventId, OnHitFly);
        playerId = owner.PlayerId;
    }

    public override void OnExit(PlayerEntity owner)
    {
        if(Module.Event.Check(PlayerBeatFlyEventArgs.EventId, OnHitFly))
        {
            Module.Event.Unsubscribe(PlayerBeatFlyEventArgs.EventId, OnHitFly);
        }
        playerId = 0;
        isBeatFly = false;
    }

    public override bool OnExecute(PlayerEntity owner)
    {
        //向玩家四个方向发射线
        Vector3 up = Vector3.up;
        Vector3 down = -Vector3.up;
        Vector3 left = -Vector3.right;
        Vector3 right = Vector3.right;
        Vector3 pos = owner.Entity.transform.position;
        RaycastHit upHit, downHit, leftHit, rightHit;
        bool isHitMapBorder = false;

        if(Ray(pos, up, out upHit))
        {
            Debug.Log("碰到上面");
            isHitMapBorder = true;
        }

        if(Ray(pos, down, out downHit))
        {
            Debug.Log("碰到下面");
            isHitMapBorder = true;
        }

        if (Ray(pos, left, out leftHit))
        {
            Debug.Log("碰到左面");
            isHitMapBorder = true;
        }

        if (Ray(pos, right, out rightHit))
        {
            Debug.Log("碰到右面");
            isHitMapBorder = true;
        }

        if(isHitMapBorder)
        {
            Debug.Log("转变状态为死亡");
            owner.Machine.ChangeState(Layer, StateType.Die);
            return true;
        }

        //击飞状态
        if(isBeatFly)
        {
            owner.Machine.ChangeState(Layer, StateType.GetHitFly);
            return true;
        }
        return false;
    }

    private bool Ray(Vector3 pos,  Vector3 dire, out RaycastHit hit)
    {
        if(Physics.Raycast(pos, dire, out hit, GlobalDefine.PLAYER_HEIGHT + 0.1f, LayerMask.GetMask("MapBorder")))
        {
            return true;
        }
        return false;
    }

    private void OnHitFly(object sender, GameEventArgs e)
    {
        PlayerBeatFlyEventArgs args = e as PlayerBeatFlyEventArgs;
        if(args.HitPlayer == playerId)
        {
            isBeatFly = true;
        }
    }
}
