using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TimerComponent;

/// <summary>
/// 技能行为
/// </summary>
public abstract class SkillAction : IReference
{
    protected PlayerEntity player;
    protected Config_Skill skillConfig;
    public Skill OwnerSkill { get; private set; }

    /// <summary>
    /// 判定区应该放到SkillActionTree里
    /// </summary>
    public Config_SkillActionTree ActionConfig { get; private set; }

    public Dictionary<int, SkillAction> SubActionDic { get; private set; }

    public virtual void Init(PlayerEntity player, Config_Skill skillConfig, Skill skill, Config_SkillActionTree actionConfig)
    {
        this.player = player;
        this.skillConfig = skillConfig;
        OwnerSkill = skill;
        ActionConfig = actionConfig;
        SubActionDic = new Dictionary<int, SkillAction>();
    }

    public void SetSubAction(Dictionary<int, SkillAction> actionDic)
    {
        SubActionDic = actionDic;
    }

    /// <summary>
    /// 进入时调用
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 每帧调用
    /// </summary>

    public virtual void OnUpdate() { }

    /// <summary>
    /// 离开时调用
    /// </summary>

    public virtual void OnExit() { }

    /// <summary>
    /// 判断什么时候结束技能
    /// 优先于skillDuration，如果这个条件满足，则不管剩余时间直接结束技能
    /// </summary>
    /// <returns></returns>
    public virtual bool EndCondition()
    {
        return false;
    }

    public void Clear()
    {
        player = null;
        skillConfig = null;
        OwnerSkill = null;
        ClearData();
    }

    public abstract void ClearData();

    /// <summary>
    /// 是否有下一个行为
    /// </summary>
    /// <returns></returns>
    public bool HasNextAction()
    {
        return SubActionDic.Count > 0;
    }
}
