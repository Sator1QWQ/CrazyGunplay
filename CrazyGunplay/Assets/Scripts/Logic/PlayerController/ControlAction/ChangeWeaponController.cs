using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponController : ControlActionBase
{
    public override ControllerType CtrlType => ControllerType.ChangeWeapon;

    public override void DoAction(PlayerController controller)
    {
        int slotIndex = controller.GetChangeWeapon();
        if(slotIndex == 0)
        {
            controller.Entity.WeaponManager.AddOrChangeSlot(slotIndex, 101);
        }
        if (slotIndex == 1)
        {
            controller.Entity.WeaponManager.AddOrChangeSlot(slotIndex, 103);
        }
        if(slotIndex == 2)
        {
            controller.Entity.WeaponManager.AddOrChangeSlot(slotIndex, 201);
        }
    }
}
