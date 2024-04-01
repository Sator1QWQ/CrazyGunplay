using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class MoveController : ControlActionBase
{
    public override ControllerType CtrlType => ControllerType.Move;

    private float mSpeed;

    public MoveController(float speed)
    {
        mSpeed = speed;
    }

    public override void DoAction(PlayerController controller)
    {
        float hori = controller.GetHorizontal();
        if(hori == -1)
        {
            controller.Entity.LookDirection = controller.Entity.transform.forward;
            controller.Gravity.AddVelocity("Left", Vector3.left * mSpeed);
            controller.Entity.transform.right = Vector3.forward;
        }
        else if(hori == 1)
        {
            controller.Entity.LookDirection = controller.Entity.transform.forward;
            controller.Gravity.AddVelocity("Right", Vector3.right * mSpeed);
            controller.Entity.transform.right = -Vector3.forward;
        }
    }
}
