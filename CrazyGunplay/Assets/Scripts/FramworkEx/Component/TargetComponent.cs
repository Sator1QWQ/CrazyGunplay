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
    public List<PlayerEntity> FindTarget(PlayerEntity owner, int targetId)
    {
        Config_FindTarget config = Config<Config_FindTarget>.Get("FindTarget", targetId);
        if (config.targetType == TargetType.NoTarget)
        {
            return null;
        }

        List<PlayerEntity> result = new List<PlayerEntity>();
        if (config.targetType == TargetType.Self)
        {
            result.Add(owner);
            return result;
        }

        IEntityGroup group = Module.Entity.GetEntityGroup("Player");
        IEntity[] entityArr = group.GetAllEntities();
        TargetSelector selector = ReferencePool.Acquire<TargetSelector>();
        selector.Init(config.targetMode, config.compare);
        int findCount = 0;

        for (int i = 0; i < entityArr.Length; i++)
        {
            Entity entity = Module.Entity.GetEntity(entityArr[i].Id);
            PlayerEntity other = entity.Logic as PlayerEntity;

            //找敌方，如果同队则不找
            if (config.targetType == TargetType.EnemyTeam && Module.Team.IsSameTeam(owner.PlayerId, other.PlayerId))
            {
                continue;
            }
            //找友方，如果不同队则不找
            else if(config.targetType == TargetType.SelfTeam && !Module.Team.IsSameTeam(owner.PlayerId, other.PlayerId))
            {
                continue;
            }

            float v = selector.GetValueBySelectMode(owner, other);
            bool compareResult = selector.Compare(other, v, config.targetValue);
            if (config.compare != CompareType.Max && config.compare != CompareType.Greater && compareResult)
            {
                if(findCount != -1 && findCount >= config.targetNum)
                {
                    break;
                }

                if(compareResult)
                {
                    result.Add(other);
                    findCount++;
                }
            }
        }

        //最小或最大
        if (config.compare == CompareType.Max)
        {
            result.Add(selector.MaxPlayer);
        }
        else if(config.compare == CompareType.Min)
        {
            result.Add(selector.MinPlayer);
        }
        ReferencePool.Release(selector);

        return result;
    }

    public List<PlayerEntity> CustomFind(PlayerEntity owner, int targetId, System.Func<bool, PlayerEntity> findRule)
    {
        Config_FindTarget config = Config<Config_FindTarget>.Get("FindTarget", targetId);
        if (config.targetType == TargetType.NoTarget)
        {
            return null;
        }

        List<PlayerEntity> result = new List<PlayerEntity>();
        if (config.targetType == TargetType.Self)
        {
            result.Add(owner);
            return result;
        }

        IEntityGroup group = Module.Entity.GetEntityGroup("Player");
        IEntity[] entityArr = group.GetAllEntities();
        int findCount = 0;

        for (int i = 0; i < entityArr.Length; i++)
        {
            Entity entity = Module.Entity.GetEntity(entityArr[i].Id);
            PlayerEntity other = entity.Logic as PlayerEntity;

            //找敌方，如果同队则不找
            if (config.targetType == TargetType.EnemyTeam && Module.Team.IsSameTeam(owner.PlayerId, other.PlayerId))
            {
                continue;
            }
            //找友方，如果不同队则不找
            else if (config.targetType == TargetType.SelfTeam && !Module.Team.IsSameTeam(owner.PlayerId, other.PlayerId))
            {
                continue;
            }

            if(config.targetNum != -1 && findCount >= config.targetNum)
            {
                Debug.Log("查找到的目标已达上限");
                break;
            }

            if (findRule(other))
            {
                result.Add(other);
                findCount++;
            }
        }

        return result;
    }
}
