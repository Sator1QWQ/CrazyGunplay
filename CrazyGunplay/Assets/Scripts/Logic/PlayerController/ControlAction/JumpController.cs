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
public class JumpController : ControlActionBase
{
    public override ControllerType CtrlType => ControllerType.Jump;
    private float jumpSpeed;

    public JumpController(float jumpSpeed)
    {
        this.jumpSpeed = jumpSpeed;
    }

    public override void DoAction(PlayerController controller)
    {
        controller.Gravity.Jump(Vector3.up * jumpSpeed);
    }
}
