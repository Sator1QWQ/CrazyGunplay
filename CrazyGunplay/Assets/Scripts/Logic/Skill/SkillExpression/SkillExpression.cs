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

    private Skill ownerSkill;
    private List<int> particleEntityList;

    public void Init(Skill owner, int expId)
    {
        ownerSkill = owner;
        Config = Config<Config_SkillExpression>.Get("SkillExpression", expId);
        particleEntityList = new List<int>();
    }

    /// <summary>
    /// 播放效果
    /// </summary>
    /// <param name="owner">调用者，不能为空</param>
    /// <param name="target">技能目标，可以为空</param>
    public void PlayClip(Transform owner, Transform target)
    {
        Debug.Log("播放效果");
        if(Config.animationSkillId != 0)
        {
            ownerSkill.OwnerPlayer.PlaySkill(Config.animationSkillId);   //播放动作
        }
        
        if (!string.IsNullOrEmpty(Config.audioPath))
        {
            string path = GlobalDefine.AUDIO_PATH + Config.audioPath + ".wav";
            Module.Audio.PlayAudio(ownerSkill.OwnerPlayer.AudioSource, path);
        }

        int particleEntityId = EntityTool.GetParticleEntityId();
        particleEntityList.Add(particleEntityId);

        if (Config.effectId != 0)
        {
            Config_Effect effectConfig = Config<Config_Effect>.Get("Effect", Config.effectId);
            Module.Entity.ShowEntity(particleEntityId, typeof(ParticleEntity), effectConfig.effectPath, "Particle", new object[] { Config.effectId, owner, target });
        }

    }

    public void StopClip()
    {
        ownerSkill.OwnerPlayer.PlaySkill(0);

        for(int i = 0; i < particleEntityList.Count; i++)
        {
            int particleEntityId = particleEntityList[i];
            //回收时可能有找不到实体id的情况
            if (Module.Entity.HasEntity(particleEntityId))
            {
                Module.Entity.HideEntity(particleEntityId);
            }
        }
        particleEntityList.Clear();
    }

    public void Clear()
    {
        StopClip();
        ownerSkill = null;
        Config = null;
    }
}
