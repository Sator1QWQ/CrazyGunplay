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
    private PlayerEntity owner;

    public override void Init(PlayerEntity player, Config_Skill skillConfig)
    {
        base.Init(player, skillConfig);
        config = Config<Config_Summon>.Get("Summon", skillConfig.values[0]);
        entitys = new List<int>();
        for (int i = 0; i < config.spawnCount; i++)
        {
            entitys.Add(EntityTool.GetSummonEntityId());
        }
        owner = player;
    }

    public override void OnUpdate()
    {
        if (Time.time >= tempTime)
        {
            tempTime = Time.time + config.spawnInterval;
            if(spawnCount < config.spawnCount)
            {
                Module.Entity.ShowEntity(entitys[index], typeof(SummonEntity), config.assetPath, "Summon", new object[] { config, owner});
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
        owner = null;
    }
}
