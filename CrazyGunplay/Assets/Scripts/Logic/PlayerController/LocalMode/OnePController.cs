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

    public override bool GetJumpInput() => Input.GetKeyDown(KeyCode.Space);

    public override bool GetMove()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            return true;
        }

        return false;
    }

    public override bool GetNormalAttack() => Input.GetKey(KeyCode.J);

    public override bool GetDushInput() => Input.GetKeyDown(KeyCode.K);

    public override int GetChangeWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 2;
        }
        return -1;
    }

    public override int GetUseSkill()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            return 1;
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            return 2;
        }

        return -1;
    }

    public override bool GetWeaponReload() => Input.GetKeyDown(KeyCode.R);
}
