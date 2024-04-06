using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		2P控制器
*/
public class TwoPController : PlayerController
{
    public TwoPController(PlayerEntity entity, SimpleGravity gravity) : base(entity, gravity)
    {
    }

    public override bool GetDushInput() => Input.GetKeyDown(KeyCode.Keypad0);

    public override float GetHorizontal()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            return 1;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            return -1;
        }

        return 0;
    }

    public override bool GetJumpInput() => Input.GetKeyDown(KeyCode.Keypad2);

    public override bool GetMove()
    {
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            return true;
        }

        return false;
    }

    public override bool GetNormalAttack() => Input.GetKey(KeyCode.Keypad1);

    public override float GetVertical() => 0;
}
