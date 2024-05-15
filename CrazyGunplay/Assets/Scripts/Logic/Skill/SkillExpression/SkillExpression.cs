using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能表现
/// </summary>
public class SkillExpression : IReference
{
    public Config_SkillExpression Config { get; private set; }

    private Skill owner;
    private int particleEntityId;
    private Transform target;

    public void Init(Skill owner, int expId)
    {
        this.owner = owner;
        Config = Config<Config_SkillExpression>.Get("SkillExpression", expId);
        particleEntityId = EntityTool.GetParticleEntityId();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">技能目标</param>
    public void PlayClip(Transform target)
    {
        Debug.Log("播放效果");
        if(Config.animationSkillId != 0)
        {
            owner.OwnerPlayer.PlaySkill(Config.animationSkillId);   //播放动作
        }
        
        if (!string.IsNullOrEmpty(Config.audioPath))
        {
            Module.Audio.PlayAudio(owner.OwnerPlayer.AudioSource, Config.audioPath);
        }

        if (Config.effectId != 0 && target != null)
        {
            Config_Effect effectConfig = Config<Config_Effect>.Get("Effect", Config.effectId);
            Module.Entity.ShowEntity(particleEntityId, typeof(ParticleEntity), effectConfig.effectPath, "Particle", new object[] { Config.effectId, target });
        }
    }

    public void StopClip()
    {
        owner.OwnerPlayer.PlaySkill(0);

        //回收时可能有找不到实体id的情况
        if (Module.Entity.HasEntity(particleEntityId))
        {
            Module.Entity.HideEntity(particleEntityId);
        }
    }

    public void Clear()
    {
        StopClip();
        owner = null;
        Config = null;
        particleEntityId = 0;
        target = null;
    }
}
