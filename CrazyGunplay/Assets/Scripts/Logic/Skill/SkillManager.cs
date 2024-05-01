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

    /// <summary>
    /// 寻找目标
    /// </summary>
    /// <param name="player"></param>
    /// <param name="targetType">目标类型</param>
    /// <param name="selectMode">选择模式</param>
    /// <param name="value">数值</param>
    /// <returns></returns>
    public PlayerEntity FindTarget(TargetType targetType, TargetSelectMode selectMode, CompareType compare, float value)
    {
        if (targetType == TargetType.NoTarget)
        {
            return null;
        }

        if (targetType == TargetType.Self)
        {
            return player;
        }

        PlayerEntity result = null;
        IEntityGroup group = Module.Entity.GetEntityGroup("Player");
        IEntity[] entityArr = group.GetAllEntities();
        TargetSelector selector = ReferencePool.Acquire<TargetSelector>();
        selector.Init(selectMode, compare);

        for (int i = 0; i < entityArr.Length; i++)
        {
            Entity entity = Module.Entity.GetEntity(entityArr[i].Id);
            PlayerEntity other = entity.Logic as PlayerEntity;

            //找敌方，如果同队则不找
            if (targetType == TargetType.EnemyTeam && Module.Team.IsSameTeam(player.PlayerId, other.PlayerId))
            {
                continue;
            }
            //找友方，如果不同队则不找
            else if(targetType == TargetType.SelfTeam && !Module.Team.IsSameTeam(player.PlayerId, other.PlayerId))
            {
                continue;
            }

            float v = selector.GetValueBySelectMode(player, other);
            bool compareResult = selector.Compare(other, v, value);
            if (compare != CompareType.Max && compare != CompareType.Greater && compareResult)
            {
                if(compareResult)
                {
                    result = other;
                    break;
                }
            }
            
        }

        //最小或最大
        if(compare == CompareType.Max)
        {
            result = selector.MaxPlayer;
        }
        else if(compare == CompareType.Min)
        {
            result = selector.MinPlayer;
        }
        ReferencePool.Release(selector);

        return result;
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
