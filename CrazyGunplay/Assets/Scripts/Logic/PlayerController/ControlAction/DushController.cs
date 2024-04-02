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
public class DushController : ControlActionBase
{
    public override ControllerType CtrlType => ControllerType.Dush;

    public override void DoAction(PlayerController controller)
    {
        controller.Gravity.AddVelocity("Dush", controller.Entity.LookDirection * 20, GlobalDefine.DUSH_TIME, false);
    }
}
