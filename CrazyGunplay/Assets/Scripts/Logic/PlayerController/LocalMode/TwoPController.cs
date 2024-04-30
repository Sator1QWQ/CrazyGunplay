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

    public override int GetChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            return 0;
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            return 1;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            return 2;
        }

        return -1;
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

    public override bool GetNormalAttack() => Input.GetKeyDown(KeyCode.Keypad1);

    public override int GetUseSkill()
    {
        if(Input.GetKeyDown(KeyCode.Keypad7))
        {
            return 1;
        }
        else if(Input.GetKeyDown(KeyCode.Keypad8))
        {
            return 2;
        }
        return -1;
    }

    public override float GetVertical() => 0;
}
