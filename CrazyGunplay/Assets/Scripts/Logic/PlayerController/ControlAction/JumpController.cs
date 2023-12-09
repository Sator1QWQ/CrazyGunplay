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

    public override void DoAction(PlayerController controller)
    {
        controller.CharacterController.SimpleMove(Vector3.up*10);
    }
}
