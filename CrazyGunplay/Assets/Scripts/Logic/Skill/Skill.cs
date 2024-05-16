using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TimerComponent;
using System.Linq;

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

    //存放技能表现 每个时机为一个key值，内层的Dictionary为某个时机的表现队列
    private Dictionary<SkillExpressionPlayTiming, Dictionary<int, SkillExpression>> expressionDic = new Dictionary<SkillExpressionPlayTiming, Dictionary<int, SkillExpression>>();

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
            int startExpressionId = expressionList[i];
            Config_SkillExpression expConfig = Config<Config_SkillExpression>.Get("SkillExpression", startExpressionId);
            Dictionary<int, SkillExpression> playQueue = new Dictionary<int, SkillExpression>();
            expressionDic.Add(expConfig.playTiming, playQueue);
            int currentExpressionId = startExpressionId;
            do
            {
                Config_SkillExpression cfg = Config<Config_SkillExpression>.Get("SkillExpression", currentExpressionId);
                SkillExpression expression = ReferencePool.Acquire<SkillExpression>();
                expression.Init(this, currentExpressionId);
                currentExpressionId = cfg.nextExpression;
                playQueue.Add(currentExpressionId, expression);
            }
            while (currentExpressionId != -1);
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
        PlayExpression(SkillExpressionPlayTiming.WhenUseSkill, OwnerPlayer.Entity.transform, OwnerPlayer.Entity.transform);
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
    /// 根据枚举来播放一段表现
    /// </summary>
    /// <param name="timing">播放时机</param>
    /// <param name="owner">调用者</param>
    /// <param name="target">特效的目标</param>
    public void PlayExpression(SkillExpressionPlayTiming timing, Transform owner, Transform target)
    {
        if(!expressionDic.ContainsKey(timing))
        {
            return;
        }

        Dictionary<int, SkillExpression> dic = expressionDic[timing];
        KeyValuePair<int, SkillExpression> first = dic.FirstOrDefault();
        int currentExpressionId = first.Key;

        //这里用闭包是为了避免使用userdata造成拆箱
        Module.Timer.AddUpdateTimer(data =>
        {
            foreach (KeyValuePair<int, SkillExpression> pairs in dic)
            {
                int expressionId = pairs.Key;
                SkillExpression expression = pairs.Value;
                if (data.continueTime >= pairs.Value.Config.delayTime)
                {
                    currentExpressionId = pairs.Value.Config.nextExpression;
                    expression.PlayClip(owner, target);
                }
            }

            //-1为表现队列播放完
            if (currentExpressionId == -1)
            {
                Module.Timer.RemoveTimer(data);
            }
        }, null, 0, -1, "SkillExpression"); //加标签，防止启用多次定时器、同时触发Clear时，无法移除原来的定时器
    }

    public void Clear()
    {
        ReferencePool.Release(Action);
        foreach(Dictionary<int, SkillExpression> queue in expressionDic.Values)
        {
            foreach(SkillExpression exp in queue.Values)
            {
                ReferencePool.Release(exp);
            }
        }
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
