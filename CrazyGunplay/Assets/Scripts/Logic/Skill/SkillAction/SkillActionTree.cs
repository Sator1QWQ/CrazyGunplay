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

    public void Init(int rootActionId, PlayerEntity player, Config_Skill skillConfig, Skill skill)
    {
        InitInternal(rootActionId, player, skillConfig, skill);
        currentActionDic.Add(rootActionId, actionDic[rootActionId]); //初始化结束时只应该有一个节点

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
        foreach(KeyValuePair<int, SkillAction> pairs in currentActionDic)
        {
            SkillAction action = pairs.Value;
            StartActionTimer(action);
        }
    }

    private void StartActionTimer(SkillAction action)
    {
        action.Enter();
        //action.ActionConfig.conditionType 暂时不做条件 有需求再做

        float continueTime = action.ActionConfig.continueTime;
        if (action.ActionConfig.continueTime == -1)
        {
            continueTime = action.OwnerSkill.Config.skillDuration;
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
        action.Update();
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
        action.Exit();

        //全部行为结束
        if(!action.HasNextAction())
        {
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
