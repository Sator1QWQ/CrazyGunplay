using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TimerComponent;

/// <summary>
/// 技能行为树
/// </summary>
public class SkillActionTree : IReference
{
    private Dictionary<int, SkillAction> actionDic = new Dictionary<int, SkillAction>();    //所有Action
    private Dictionary<int, SkillAction> currentActionDic = new Dictionary<int, SkillAction>(); //当前正在执行的Action
    private int rootActionId = 0;

    /// <summary>
    /// 技能行为树状态
    /// </summary>
    public SkillActionTreeState TreeState { get; private set; }

    public void Init(int rootActionId, PlayerEntity player, Config_Skill skillConfig, Skill skill)
    {
        InitInternal(rootActionId, player, skillConfig, skill);
        this.rootActionId = rootActionId;

        //设置子Action结构
        foreach (KeyValuePair<int, SkillAction> pairs in actionDic)
        {
            Config_SkillActionTree tree = Config<Config_SkillActionTree>.Get("SkillActionTree", pairs.Key);
            Dictionary<int, SkillAction> subActionDic = new Dictionary<int, SkillAction>();
            for(int i = 0; i < tree.nextActionList.Count; i++)
            {
                int nextId = tree.nextActionList[i];
                if(nextId == -1)
                {
                    break;
                }

                subActionDic.Add(nextId, actionDic[nextId]);
            }
            pairs.Value.SetSubAction(subActionDic);
        }
        TreeState = SkillActionTreeState.NotRun;
    }

    //初始化actionDic
    private void InitInternal(int rootActionId, PlayerEntity player, Config_Skill skillConfig, Skill skill)
    {
        Config_SkillActionTree tree = Config<Config_SkillActionTree>.Get("SkillActionTree", rootActionId);
        SkillAction action = CreateAction(tree.actionType);
        action.Init(player, skillConfig, skill, tree);
        actionDic.Add(tree.id, action);

        for (int i = 0; i < tree.nextActionList.Count; i++)
        {
            int nextId = tree.nextActionList[i];
            if (nextId == -1)
            {
                break;
            }

            InitInternal(nextId, player, skillConfig, skill);    //深度优先递归
        }
    }

    private SkillAction CreateAction(SkillCastAction actionType)
    {
        SkillAction action = null;

        if (actionType == SkillCastAction.GetWeapon)
        {
            action = ReferencePool.Acquire<GetWeaponAction>();
        }
        else if (actionType == SkillCastAction.Summon)
        {
            action = ReferencePool.Acquire<SummonAction>();
        }

        return action;
    }

    public void StartAction()
    {
        TreeState = SkillActionTreeState.Running;

        //每次使用技能行为时，currentActionDic必须没有数据，如果有则说明有问题
        //初始化结束时只应该有一个节点
        currentActionDic.Add(rootActionId, actionDic[rootActionId]);
        foreach (KeyValuePair<int, SkillAction> pairs in currentActionDic)
        {
            SkillAction action = pairs.Value;
            StartActionTimer(action);
        }
    }

    private void StartActionTimer(SkillAction action)
    {
        if(!currentActionDic.ContainsKey(action.ActionConfig.id))
        {
            currentActionDic.Add(action.ActionConfig.id, action);
        }

        action.OnEnter();
        //action.ActionConfig.conditionType 暂时不做条件 有需求再做

        float continueTime = 0;
        switch(action.ActionConfig.timerContinueType)
        {
            case TimerContinueType.Fixed:
                continueTime = action.ActionConfig.continueTime;
                break;

            //SkillAction中的Custom1表示根据技能时间来确定定时器时间
            case TimerContinueType.Custom1:
                continueTime = action.OwnerSkill.Config.skillDuration;
                break;

            //Custom2为自定义时间，必须要确定结束定时器的条件
            case TimerContinueType.Custom2:
                continueTime = -1;
                break;
        }
        Module.Timer.AddUpdateTimer(DoActionTimer, EndActionTimer, action.ActionConfig.delayTime, continueTime, action, "SkillAction");
    }

    private void DoActionTimer(TimerData data)
    {
        SkillAction action = data.userdata as SkillAction;
        if(!action.OwnerSkill.IsSkillRunning)
        {
            Debug.LogError("技能已经结束，但是Action还在执行 配置有错误！");
            return;
        }
        action.OnUpdate();
        if (action.EndCondition())
        {
            Debug.Log("技能结束条件已满足");
            Module.Timer.EndTimer(data);
        }
        else
        {
            Debug.Log("技能结束条件不满足");
        }
    }

    private void EndActionTimer(TimerData data)
    {
        SkillAction action = data.userdata as SkillAction;
        action.OnExit();
        if(currentActionDic.ContainsKey(action.ActionConfig.id))
        {
            currentActionDic.Remove(action.ActionConfig.id);
        }

        if(!action.HasNextAction())
        {
            if (currentActionDic.Count == 0)
            {
                TreeState = SkillActionTreeState.AllEnd;
            }
            return;
        }

        foreach(KeyValuePair<int, SkillAction> pairs in action.SubActionDic)
        {
            //执行子行为
            StartActionTimer(pairs.Value);
        }
    }

    public void Clear()
    {
        actionDic.Clear();
        currentActionDic.Clear();
        Module.Timer.RemoveTimersByTag("SkillAction");
    }
}
