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
    private Transform owner;
    private Config_Effect config;
    private float tempTime = 0;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        particle = GetComponent<ParticleSystem>();
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        if(userData == null)
        {
            return;
        }

        object[] objs = userData as object[];
        int id = (int)objs[0];
        config = Config<Config_Effect>.Get("Effect", id);
        owner = objs[1] as Transform;
        target = objs[2] as Transform;
        if (config.isEffectFollow)
        {
            if (target == null)
            {
                Debug.LogError("特效配置为跟随目标，但是没有目标参数！检查调用PlayClip时传入的参数");
                return;
            }
            Entity.transform.SetParent(target, false);
            Entity.transform.position = target.position;
        }
        else
        {
            Entity.transform.position = owner.position;
        }
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if(config == null)
        {
            return;
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
