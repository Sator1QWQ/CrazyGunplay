using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 召唤物实体
/// 攻击目标
/// </summary>
public class SummonEntity : EntityLogic
{
    private Config_Summon config;
    private PlayerEntity skillOwner;
    private PlayerEntity targetPlayer;
    private Vector3 startPoint;
    private Vector3 endPoint;

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        object[] objs = userData as object[];
        config = objs[0] as Config_Summon;
        skillOwner = objs[1] as PlayerEntity;

        //每个召唤物只找一次，之后目标不会再变
        targetPlayer = skillOwner.SkillManager.FindTarget(config.castTarget, config.targetMode, config.compare, config.targetValue);

        if (config.castTarget == TargetType.EnemyTeam && targetPlayer != null)
        {
            //从玩家头顶起
            startPoint = new Vector3(Random.Range(0, 5) + targetPlayer.Entity.transform.position.x, GlobalDefine.MAP_MAX_HEIGHT, 0);
        }
        endPoint = targetPlayer.Entity.transform.position;
        Entity.transform.position = startPoint;
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);

        if(config.isContinueFollow)
        {
            endPoint = targetPlayer.Entity.transform.position;
        }

        if (targetPlayer != null)
        {
            Entity.transform.right = endPoint - Entity.transform.position;
            Entity.transform.Translate(new Vector3(config.moveSpeed * Time.deltaTime, 0, 0));
        }
    }

    protected override void OnRecycle()
    {
        base.OnRecycle();
        config = null;
        skillOwner = null;
        targetPlayer = null;
        startPoint = default;
        endPoint = default;
    }
}
