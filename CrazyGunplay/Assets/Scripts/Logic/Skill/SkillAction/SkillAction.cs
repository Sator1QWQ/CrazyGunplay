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

    public virtual void Init(PlayerEntity player, Config_Skill skillConfig)
    {
        this.player = player;
        this.skillConfig = skillConfig;
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
    /// </summary>
    /// <returns></returns>
    public virtual bool EndCondition(TimerData timer)
    {
        if(timer.duration != -1)
        {
            return timer.remainTime >= 0;
        }

        //技能无限时间，需要子类实现条件
        return false;
    }

    public void Clear()
    {
        player = null;
        skillConfig = null;
        ClearData();
    }

    public abstract void ClearData();
}
