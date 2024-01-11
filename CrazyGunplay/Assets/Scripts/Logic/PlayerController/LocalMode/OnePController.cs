using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		1P控制器(PC端)
*/
public class OnePController : PlayerController
{
    public OnePController(PlayerEntity entity, SimpleGravity gravity) : base(entity, gravity)
    {
    }

    public override float GetHorizontal()
    {
        if(Input.GetKey(KeyCode.A))
        {
            return -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            return 1;
        }

        return 0;
    }

    public override float GetVertical()
    {
        return 0;
    }

    public override bool GetJump() => Input.GetKeyDown(KeyCode.Space);

    public override bool GetMove()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            return true;
        }

        return false;
    }

    public override bool GetNormalAttack() => Input.GetKeyDown(KeyCode.J);

    public override bool GetDush() => Input.GetKeyDown(KeyCode.K);
}
