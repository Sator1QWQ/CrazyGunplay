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

    public Config_SkillActionTree ActionConfig { get; private set; }

    public Dictionary<int, SkillAction> SubActionDic { get; private set; }

    /// <summary>
    /// 命中数据
    /// </summary>
    public HitData HitData { get; private set; }

    private float tempTime;

    public virtual void Init(PlayerEntity player, Config_Skill skillConfig, Skill skill, Config_SkillActionTree actionConfig)
    {
        this.player = player;
        this.skillConfig = skillConfig;
        OwnerSkill = skill;
        ActionConfig = actionConfig;
        SubActionDic = new Dictionary<int, SkillAction>();
        HitData = CreateHitData();
    }

    public void SetSubAction(Dictionary<int, SkillAction> actionDic)
    {
        SubActionDic = actionDic;
    }

    /// <summary>
    /// 进入时调用
    /// </summary>
    public virtual void OnEnter() 
    {
        if (ActionConfig.areaTriggerType == ActionAreaTriggerTiming.OnActionEnter)
        {
            Module.HitArea.HitPlayerAction(OwnerSkill.OwnerPlayer, OwnerSkill.Config.findTargetId, ActionConfig.areaId, player.Entity.transform.position, HitData);
        }
    }

    /// <summary>
    /// 每帧调用
    /// </summary>
    public virtual void OnUpdate() 
    {
        if (ActionConfig.areaTriggerType == ActionAreaTriggerTiming.OnActionUpdate)
        {
            tempTime += Time.deltaTime;
            if(tempTime >= ActionConfig.areaTriggerInterval)
            {
                Module.HitArea.HitPlayerAction(OwnerSkill.OwnerPlayer, OwnerSkill.Config.findTargetId, ActionConfig.areaId, player.Entity.transform.position, HitData);
                tempTime = 0;
            }
        }
    }

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
        HitData = default;
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

    private HitData CreateHitData()
    {
        HitData data = new HitData();
        data.dealerId = player.PlayerId;
        data.buffList = new List<BuffValue>();
        data.hitType = ActionConfig.getHitType;
        for (int i = 0; i < ActionConfig.buffIdList.Count; i++)
        {
            BuffValue value = new BuffValue();
            value.buffId = ActionConfig.buffIdList[i];
            value.value = ActionConfig.buffValueList[i];
            value.duration = ActionConfig.buffContinueList[i];
            data.buffList.Add(value);
        }
        return data;
    }
}
