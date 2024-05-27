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
    private PlayerEntity ownerPlayer;
    private Skill skill;
    private PlayerEntity targetPlayer;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private List<Vector3> routeList;    //移动路径
    private int curRouteIndex;
    private SkillAction ownerAction;

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        object[] objs = userData as object[];
        config = objs[0] as Config_Summon;
        ownerPlayer = objs[1] as PlayerEntity;
        skill = objs[2] as Skill;
        ownerAction = objs[3] as SkillAction;

        //每个召唤物只找一次，之后目标不会再变
        targetPlayer = skill.TargetList[0];
        if(targetPlayer == null)
        {
            return;
        }

        Config_FindTarget targetConfig = Config<Config_FindTarget>.Get("FindTarget", skill.Config.findTargetId);
        if (targetConfig.targetType == TargetType.EnemyTeam && targetPlayer != null)
        {
            //从玩家头顶起
            //startPoint = new Vector3(Random.Range(0, 5) + targetPlayer.Entity.transform.position.x, GlobalDefine.MAP_MAX_HEIGHT, 0);
            startPoint = new Vector3(targetPlayer.Entity.transform.position.x, GlobalDefine.MAP_MAX_HEIGHT, 0);
        }
        endPoint = targetPlayer.Entity.transform.position;
        Entity.transform.position = startPoint;

        BezierTool.RandomBezierData data = new BezierTool.RandomBezierData();
        data.routeCount = 20;
        data.controlPointCount = 4;
        data.range = 5f;
        data.isControlSameWithNormal = false;
        data.normal = Vector3.Cross(endPoint - startPoint, Vector3.forward).normalized;
        routeList = BezierTool.GetRandomLine(startPoint, endPoint, data);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if(targetPlayer == null)
        {
            return;
        }

        if(config.isContinueFollow)
        {
            endPoint = targetPlayer.Entity.transform.position;
        }

        if (targetPlayer != null && curRouteIndex < routeList.Count)
        {
            Entity.transform.right = endPoint - Entity.transform.position;
            Entity.transform.position = Vector3.MoveTowards(Entity.transform.position, routeList[curRouteIndex], config.moveSpeed * Time.deltaTime);
            //Entity.transform.Translate(new Vector3(config.moveSpeed * Time.deltaTime, 0, 0));
            if (Vector3.Distance(Entity.transform.position, routeList[curRouteIndex]) <= 0.01f)
            {
                curRouteIndex++;
            }
        }

        if(curRouteIndex >= routeList.Count)
        {
            if(config.isContinueFollow)
            {
                Entity.transform.right = endPoint - Entity.transform.position;
            }
            Entity.transform.Translate(config.moveSpeed * Time.deltaTime, 0, 0, Space.Self);
        }

        RaycastHit hit;
        PlayerEntity hitPlayer;
        RayHitPlayerResult hitResult = PhysicsExtension.RayCastHitPlayer(Entity.transform.position, Entity.transform.right, out hit, GlobalDefine.BULLET_RAY_DISTANCE, LayerMask.GetMask("Player", "Wall", "Floor"), ownerPlayer.PlayerId, skill.Config.findTargetId, out hitPlayer);

        if (hitResult == RayHitPlayerResult.HitTargetPlayer)
        {
            if (ownerAction.ActionConfig.areaTriggerType == ActionAreaTriggerTiming.Custom)
            {
                List<PlayerEntity> list = new List<PlayerEntity>();
                list.Add(hitPlayer);
                Module.HitArea.HitPlayerAction(ownerPlayer, skill.Config.findTargetId, ownerAction.ActionConfig.areaId, Entity.transform.position, ownerAction.HitData, list);
            }
        }

        if (hitResult != RayHitPlayerResult.HitNone && hitResult != RayHitPlayerResult.HitNotTargetPlayer)
        {
            skill.PlayExpression(SkillExpressionPlayTiming.WhenHit, Entity.transform, targetPlayer.Entity.transform);
            (ownerAction as SummonAction).OnEntityHit();
            Module.Entity.HideEntity(Entity);
        }
    }

    protected override void OnRecycle()
    {
        base.OnRecycle();
        config = null;
        ownerPlayer = null;
        targetPlayer = null;
        startPoint = default;
        endPoint = default;
        routeList = null;
        curRouteIndex = 0;
        skill = null;
        ownerAction = null;
    }
}
