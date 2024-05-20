using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeaponAction : SkillAction
{
    private Weapon weapon;

    public override void Init(PlayerEntity player, Config_Skill config, Skill skill, Config_SkillActionTree actionConfig)
    {
        base.Init(player, config, skill, actionConfig);
    }

    public override void ClearData()
    {
        weapon = null;
    }

    public override void OnEnter()
    {
        if (!player.WeaponManager.HasWeapon(GlobalDefine.SKILL_WEAPON_SLOT))
        {
            player.WeaponManager.AddOrChangeSlot(GlobalDefine.SKILL_WEAPON_SLOT, (int)ActionConfig.actionValue[0]);
        }
        else
        {
            player.WeaponManager.ChangeSlot(GlobalDefine.SKILL_WEAPON_SLOT);
        }
        weapon = player.WeaponManager.GetWeapon(GlobalDefine.SKILL_WEAPON_SLOT);
    }

    public override void OnExit()
    {
        //技能结束时移除武器
        player.WeaponManager.RemoveWeapon(GlobalDefine.SKILL_WEAPON_SLOT);
    }

    public override bool EndCondition()
    {
        return weapon.MainMagazine == 0 && weapon.SpareMagazine == 0;
    }
}
