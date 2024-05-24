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
    private float defaultSpeed;

    public MoveController(float speed, PlayerEntity entity)
    {
        defaultSpeed = speed;
        mSpeed = defaultSpeed;
        entity.OnBuffDataChange += OnBuffDataChange;
    }

    private void OnBuffDataChange(PlayerEntity player)
    {
        float buffScale = Module.PlayerData.GetBuffData(player.PlayerId).moveSpeedScale;
        mSpeed = defaultSpeed * buffScale;
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
