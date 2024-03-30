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
        //向玩家四个方向发射线
        Vector3 up = owner.Entity.transform.up;
        Vector3 down = -owner.Entity.transform.up;
        Vector3 left = -owner.Entity.transform.right;
        Vector3 right = owner.Entity.transform.right;
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
}
