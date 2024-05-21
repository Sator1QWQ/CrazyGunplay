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
                Module.Entity.ShowEntity(entitys[index], typeof(SummonEntity), config.assetPath, "Summon", new object[] { config, ownerPlayer, OwnerSkill});
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
    }

    //public override bool EndCondition(TimerComponent.TimerData timer)
    //{
    //    if(spawnCount > config.spawnCount)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

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
    }
}
