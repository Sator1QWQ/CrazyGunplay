using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeaponAction : SkillAction
{
    private Weapon weapon;

    public override void Init(PlayerEntity player, Config_Skill config)
    {
        base.Init(player, config);
    }

    public override void ClearData()
    {
        weapon = null;
    }

    public override void OnEnter()
    {
        Debug.Log("吓我一跳释放武器");

        if (!player.WeaponManager.HasWeapon(GlobalDefine.SKILL_WEAPON_SLOT))
        {
            player.WeaponManager.AddOrChangeSlot(GlobalDefine.SKILL_WEAPON_SLOT, skillConfig.values[0]);
        }
        else
        {
            player.WeaponManager.ChangeSlot(GlobalDefine.SKILL_WEAPON_SLOT);
        }
        weapon = player.WeaponManager.GetWeapon(GlobalDefine.SKILL_WEAPON_SLOT);
    }

    public override void OnExit()
    {
        Debug.Log("技能结束");
        
        //技能结束时移除武器
        player.WeaponManager.RemoveWeapon(GlobalDefine.SKILL_WEAPON_SLOT);
    }

    public override bool EndCondition(TimerComponent.TimerData timer)
    {
        return weapon.MainMagazine == 0 && weapon.SpareMagazine == 0;
    }
}
