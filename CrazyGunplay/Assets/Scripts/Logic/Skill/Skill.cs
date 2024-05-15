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

    //第一个int是表现队列的第一个id， 第二个int是每个表现id
    private Dictionary<int, Dictionary<int, SkillExpression>> expressionDic = new Dictionary<int, Dictionary<int, SkillExpression>>();

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
        else if(Config.skillAction == SkillCastAction.Summon)
        {
            Action = ReferencePool.Acquire<SummonAction>();
        }

        if(Action != null)
        {
            Action.Init(ownerPlayer, Config, this);
        }

        //初始化技能表现
        List<int> expressionList = Config.expressionList;
        for(int i = 0; i < expressionList.Count; i++)
        {
            int firstExpression = expressionList[i];
            int currentExpression = firstExpression;
            expressionDic.Add(firstExpression, new Dictionary<int, SkillExpression>());
            do
            {
                Config_SkillExpression expCfg = Config<Config_SkillExpression>.Get("SkillExpression", currentExpression);
                SkillExpression expression = ReferencePool.Acquire<SkillExpression>();
                expression.Init(this, currentExpression);
                expressionDic[firstExpression].Add(currentExpression, expression);
                currentExpression = expCfg.nextExpression;
            }
            while (currentExpression != -1);
        }

        Debug.Log($"玩家{ownerPlayer.PlayerId}初始化技能{skillId}");
    }

    public void UseSkill()
    {
        if(Config.coolingTiming == SkillCoolingTiming.WhenUseSkill)
        {
            UseTimeSpan = Time.time;
        }
        Debug.Log($"玩家{OwnerPlayer.PlayerId}使用技能{Config.id}");
        Action.OnEnter();
        skillTimer = Module.Timer.AddUpdateTimer(SkillTimer, EndSkillTimer, 0, Config.skillDuration);
        PlayExpression(Config.expressionList[0], OwnerPlayer.Entity.transform); //规定索引0为动画
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

    /// <summary>
    /// 播放技能表现
    /// </summary>
    /// <param name="startExpressionId">表现队列的一个id</param>
    /// <param name="target">表现的目标</param>
    public void PlayExpression(int startExpressionId, Transform target)
    {
        int currentExpressionId = startExpressionId;
        Module.Timer.AddUpdateTimer(data =>
        {
            Dictionary<int, SkillExpression> dic = expressionDic[startExpressionId];    //这里用闭包是为了避免使用userdata造成拆箱
            foreach(KeyValuePair<int, SkillExpression> pairs in dic)
            {
                int expressionId = pairs.Key;
                SkillExpression expression = pairs.Value;
                if(data.continueTime >= pairs.Value.Config.delayTime)
                {
                    currentExpressionId = pairs.Value.Config.nextExpression;
                    expression.PlayClip(target);
                }
            }

            //-1为表现队列播放完
            if(currentExpressionId == -1)
            {
                Module.Timer.RemoveTimer(data);
            }
        }, null, 0, -1, "SkillExpression"); //加标签，防止启用多次定时器时，同时触发Clear，这时找不到原来的定时器
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
        expressionDic.Clear();
        Module.Timer.RemoveTimersByTag("SkillExpression");
    }
}
