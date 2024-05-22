using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 召唤行为
/// </summary>
public class SummonAction : SkillAction
{
    private Config_Summon config;
    private List<int> entitys;
    private int index;
    private float spawnCount;
    private float tempTime;
    private PlayerEntity ownerPlayer;
    private int hitCount;

    public override void Init(PlayerEntity player, Config_Skill skillConfig, Skill skill, Config_SkillActionTree actionConfig)
    {
        base.Init(player, skillConfig, skill, actionConfig);
        config = Config<Config_Summon>.Get("Summon", (int)(long)actionConfig.actionValue[0]);
        entitys = new List<int>();
        for (int i = 0; i < config.spawnCount; i++)
        {
            entitys.Add(EntityTool.GetSummonEntityId());
        }
        ownerPlayer = player;
    }

    public override void OnUpdate()
    {
        if (Time.time >= tempTime)
        {
            tempTime = Time.time + config.spawnInterval;
            if(spawnCount < config.spawnCount)
            {
                Module.Entity.ShowEntity(entitys[index], typeof(SummonEntity), config.assetPath, "Summon", new object[] { config, ownerPlayer, OwnerSkill, this});
                spawnCount++;
            }
            index++;
        }
    }

    public override void OnExit()
    {
        for (int i = 0; i < entitys.Count; i++)
        {
            //有实体不存在的情况，必需要判断
            if(Module.Entity.HasEntity(entitys[i]))
            {
                Module.Entity.HideEntity(entitys[i]);
            }
        }
        index = 0;
        spawnCount = 0;
        tempTime = 0;
        hitCount = 0;
    }

    //全部实体命中时，定时器结束
    public override bool EndCondition()
    {
        return hitCount >= entitys.Count;
    }

    /// <summary>
    /// 当实体命中时
    /// </summary>
    public void OnEntityHit()
    {
        //这里只判断有多少个实体命中了，如果存在1个实体命中多次，则有错误
        hitCount++;
    }

    public override void ClearData()
    {
        config = null;
        for(int i = 0; i < entitys.Count; i++)
        {
            if(Module.Entity.HasEntity(entitys[i]))
            {
                Module.Entity.HideEntity(entitys[i]);
            }
        }
        entitys = null;
        index = 0;
        spawnCount = 0;
        tempTime = 0;
        ownerPlayer = null;
        hitCount = 0;
    }
}
