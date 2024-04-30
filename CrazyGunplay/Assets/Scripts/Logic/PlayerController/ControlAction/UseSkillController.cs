using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkillController : ControlActionBase
{
    public override ControllerType CtrlType => ControllerType.UseSkill;

    public override void DoAction(PlayerController controller)
    {
        int skillSlot = controller.GetUseSkill();
        if(skillSlot != -1)
        {
            int index = skillSlot - 1;
            Config_Character config = Config<Config_Character>.Get("Character", controller.Entity.Data.heroId);
            int skillId = config.initSkill[index];
            controller.Entity.SkillManager.TryUseSkill(skillId);
        }
    }
}
