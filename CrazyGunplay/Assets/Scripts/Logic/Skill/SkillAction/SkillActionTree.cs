using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能行为树
/// </summary>
public class SkillActionTree
{
    private Dictionary<int, SkillAction> actionDic = new Dictionary<int, SkillAction>();    //所有Action id有问题
    private Dictionary<int, SkillAction> currentActionDic = new Dictionary<int, SkillAction>(); //当前正在执行的Action id有问题

    public void Init(int rootActionId, PlayerEntity player, Config_Skill skillConfig, Skill skill)
    {
        Config_SkillActionTree tree = Config<Config_SkillActionTree>.Get("SkillActionTree", rootActionId);
        for (int i = 0; i < tree.nextActionList.Count; i++)
        {
            int currentId = tree.nextActionList[i];
            if (currentId == -1)
            {
                break;
            }

            SkillAction action = CreateAction(tree.actionType);
            action.Init(player, skillConfig, skill, tree);
            actionDic.Add(currentId, action);
            Init(currentId, player, skillConfig, skill);    //深度优先递归
        }

        currentActionDic.Add(tree.id, actionDic[rootActionId]); //初始化结束时只应该有一个节点
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

    public void OnEnter()
    {
        
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
