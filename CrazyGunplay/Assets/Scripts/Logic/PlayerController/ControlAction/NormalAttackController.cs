using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		普通攻击
*/
public class NormalAttackController : ControlActionBase
{
    public override ControllerType CtrlType => ControllerType.NomralAttack;

    public override void DoAction(PlayerController controller)
    {
        Weapon weapon = Module.Weapon.GetWeapon(controller.Entity.Data.WeaponId);
        weapon.Attack();
    }
}
