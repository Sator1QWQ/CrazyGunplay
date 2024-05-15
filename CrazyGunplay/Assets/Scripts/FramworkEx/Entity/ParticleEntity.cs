using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 粒子实体
/// </summary>
public class ParticleEntity : EntityLogic
{
    private ParticleSystem particle;
    private Transform target;
    private Config_Effect config;
    private float tempTime = 0;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        object[] objs = userData as object[];
        int id = (int)objs[0];
        config = Config<Config_Effect>.Get("Effect", id);
        particle = GetComponent<ParticleSystem>();
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        object[] objs = userData as object[];
        target = objs[1] as Transform;
        Entity.transform.position = target.transform.position;
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);

        if(config.isEffectFollow)
        {
            Entity.transform.position = target.transform.position;
        }

        //时间到了自动删除
        if(config.effectDeleteTiming == SkillEffectDeleteTiming.WhenEffectEnd)
        {
            tempTime += Time.deltaTime;
            if (tempTime >= particle.main.duration)
            {
                tempTime = 0;
                Module.Entity.HideEntity(Entity);
                return;
            }
        }
    }


}
