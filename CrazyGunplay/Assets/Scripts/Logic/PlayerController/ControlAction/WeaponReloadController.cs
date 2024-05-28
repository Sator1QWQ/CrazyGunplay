using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器换弹
/// </summary>
public class WeaponReloadController : ControlActionBase
{
    public override ControllerType CtrlType => ControllerType.WeaponReload;

    public override void DoAction(PlayerController controller)
    {
        Debug.Log("控制器 换弹中");
    }
}
