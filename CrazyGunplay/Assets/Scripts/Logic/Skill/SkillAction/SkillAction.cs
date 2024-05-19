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
    protected Config_SkillActionTree actionConfig;
    protected Skill skill;

    public virtual void Init(PlayerEntity player, Config_Skill skillConfig, Skill skill, Config_SkillActionTree actionConfig)
    {
        this.player = player;
        this.skillConfig = skillConfig;
        this.skill = skill;
        this.actionConfig = actionConfig;
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
    public virtual bool EndCondition(TimerData timer)
    {
        return false;
    }

    public void Clear()
    {
        player = null;
        skillConfig = null;
        skill = null;
        ClearData();
    }

    public abstract void ClearData();
}
