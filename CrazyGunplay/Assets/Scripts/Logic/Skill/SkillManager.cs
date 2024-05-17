using GameFramework;
using GameFramework.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class SkillManager
{
    private Dictionary<int, Skill> skillDic = new Dictionary<int, Skill>();
    private PlayerEntity player;

    public SkillManager(PlayerEntity player)
    {
        this.player = player;
    }

    public void AddSkill(int skillId)
    {
        Skill skill = ReferencePool.Acquire<Skill>();
        skill.InitSkill(skillId, player);
        skillDic.Add(skillId, skill);
    }

    public Skill GetSkill(int skillId)
    {
        return skillDic[skillId];
    }

    public bool TryUseSkill(int skillId)
    {
        if(!SkillCondition(skillId))
        {
            return false;
        }

        skillDic[skillId].UseSkill();
        return true;
    }

    private bool SkillCondition(int skillId)
    {
        Skill skill = skillDic[skillId];
        Config_Skill cfg = skill.Config;

        //冷却
        if(skill.UseTimeSpan > 0 && Time.time - skill.UseTimeSpan < cfg.coolingTime)
        {
            Debug.Log("技能冷却中");
            return false;
        }

        //激活条件
        if(cfg.activeCondition[0] != 0 && !SkillActiveCondition(skillId))
        {
            Debug.Log("技能不满足激活条件");
            return false;
        }

        //当前技能正在使用中
        if(skill.IsSkillRunning)
        {
            Debug.Log("当前技能正在使用");
            return false;
        }

        //能不能找到目标

        return true;
    }

    //一般只有大招才需要激活条件
    private bool SkillActiveCondition(int skillId)
    {
        Skill skill = skillDic[skillId];
        Config_Skill cfg = skill.Config;
        SkillActiveConditionType type = (SkillActiveConditionType)cfg.activeCondition[0];
        int value = cfg.activeCondition[1];
        if (type == SkillActiveConditionType.Time)
        {
            //激活时间>当前时间
            if(skill.StartTime + value > Time.time)
            {
                return false;
            }
        }
        else if(type == SkillActiveConditionType.Damage)
        {
            return false;
        }

        return true;
    }

    public void Clear()
    {
        foreach(KeyValuePair<int, Skill> pair in skillDic)
        {
            ReferencePool.Release(pair.Value);
        }
        skillDic.Clear();
        skillDic = null;
        player = null;
    }
}
