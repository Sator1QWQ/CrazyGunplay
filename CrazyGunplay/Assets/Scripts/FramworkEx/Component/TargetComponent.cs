using GameFramework;
using GameFramework.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 目标选择组件
/// </summary>
public class TargetComponent : GameFrameworkComponent
{
    /// <summary>
    /// 寻找目标
    /// </summary>
    /// <param name="player"></param>
    /// <param name="targetType">目标类型</param>
    /// <param name="selectMode">选择模式</param>
    /// <param name="value">数值</param>
    /// <returns></returns>
    public PlayerEntity FindTarget(PlayerEntity owner, TargetType targetType, TargetSelectMode selectMode, CompareType compare, float value)
    {
        if (targetType == TargetType.NoTarget)
        {
            return null;
        }

        if (targetType == TargetType.Self)
        {
            return owner;
        }

        PlayerEntity result = null;
        IEntityGroup group = Module.Entity.GetEntityGroup("Player");
        IEntity[] entityArr = group.GetAllEntities();
        TargetSelector selector = ReferencePool.Acquire<TargetSelector>();
        selector.Init(selectMode, compare);

        for (int i = 0; i < entityArr.Length; i++)
        {
            Entity entity = Module.Entity.GetEntity(entityArr[i].Id);
            PlayerEntity other = entity.Logic as PlayerEntity;

            //找敌方，如果同队则不找
            if (targetType == TargetType.EnemyTeam && Module.Team.IsSameTeam(owner.PlayerId, other.PlayerId))
            {
                continue;
            }
            //找友方，如果不同队则不找
            else if(targetType == TargetType.SelfTeam && !Module.Team.IsSameTeam(owner.PlayerId, other.PlayerId))
            {
                continue;
            }

            float v = selector.GetValueBySelectMode(owner, other);
            bool compareResult = selector.Compare(other, v, value);
            if (compare != CompareType.Max && compare != CompareType.Greater && compareResult)
            {
                if(compareResult)
                {
                    result = other;
                    break;
                }
            }
            
        }

        //最小或最大
        if(compare == CompareType.Max)
        {
            result = selector.MaxPlayer;
        }
        else if(compare == CompareType.Min)
        {
            result = selector.MinPlayer;
        }
        ReferencePool.Release(selector);

        return result;
    }
}
