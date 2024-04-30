using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TimerComponent;

public class Skill : IReference
{
    public Config_Skill Config { get; private set; }

    /// <summary>
    /// 释放该技能的玩家
    /// </summary>
    public PlayerEntity OwnerPlayer { get; private set; }

    /// <summary>
    /// 使用技能时的开始时间
    /// </summary>
    public float UseTimeSpan { get; private set; }

    /// <summary>
    /// 每次初始化时设置时间
    /// </summary>
    public float StartTime { get; private set; }

    public SkillAction Action { get; private set; }

    private TimerData skillTimer;

    /// <summary>
    /// 初始化技能
    /// </summary>
    public void InitSkill(int skillId, PlayerEntity ownerPlayer)
    {
        Config = Config<Config_Skill>.Get("Skill", skillId);
        OwnerPlayer = ownerPlayer;
        StartTime = Time.time;

        if(Config.skillAction == SkillCastAction.GetWeapon)
        {
            Action = ReferencePool.Acquire<GetWeaponAction>();
        }

        if(Action != null)
        {
            Action.Init(ownerPlayer, Config);
        }   
    }

    public void UseSkill()
    {
        if(Config.coolingTiming == SkillCoolingTiming.WhenUseSkill)
        {
            UseTimeSpan = Time.time;
        }

        Action.OnEnter();
        skillTimer = Module.Timer.AddUpdateTimer(SkillTimer, EndSkillTimer, 0, Config.skillDuration);
        //玩家状态修改
    }

    private void SkillTimer(TimerData data)
    {
        Action.OnUpdate();
        if(Action.EndCondition(data))
        {
            Debug.Log("技能结束条件已满足");
            Module.Timer.EndTimer(data);
        }
        else
        {
            Debug.Log("技能结束条件不满足");
        }
    }

    private void EndSkillTimer(TimerData data)
    {
        Action.OnExit();
        if(Config.coolingTiming == SkillCoolingTiming.WhenEndSkill)
        {
            UseTimeSpan = Time.time;
        }
    }

    public void Clear()
    {
        ReferencePool.Release(Action);
        Action = null;
        Module.Timer.RemoveTimer(skillTimer);
        skillTimer = null;
        Config = null;
        OwnerPlayer = null;
        UseTimeSpan = 0;
        StartTime = 0;
    }
}
