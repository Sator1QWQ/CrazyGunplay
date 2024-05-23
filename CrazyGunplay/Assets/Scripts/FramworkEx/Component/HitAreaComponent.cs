using GameFramework.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 命中区域判定组件
/// </summary>
public class HitAreaComponent : GameFrameworkComponent
{
    /// <summary>
    /// 执行命中操作，不一定是伤害命中，也可以是其他的回血等命中
    /// </summary>
    /// <param name="owner">玩家</param>
    /// <param name="targetId">目标表id</param>
    /// <param name="areaId">范围表id</param>
    /// <param name="startPoint"></param>
    public void HitPlayerAction(PlayerEntity owner, int targetId, int areaId, Vector3 startPoint, HitData data)
    {
        List<PlayerEntity> result = GetHitResult(owner, targetId, areaId, startPoint, data);
        for (int i = 0; i < result.Count; i++)
        {
            HitData newData = new HitData();
            newData.buffList = data.buffList;
            newData.dealerId = data.dealerId;
            newData.hitType = data.hitType;
            newData.receiverId = result[i].PlayerId;    //旧data的receiverId无法确定，只有在这里才能确定
            HitEventArgs args = HitEventArgs.Create(newData);
            Module.Event.FireNow(this, args);
        }
    }

    /// <summary>
    /// 仅检测是否命中玩家，不会发送广播
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="targetId"></param>
    /// <param name="areaId"></param>
    /// <param name="startPoint"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool IsHitPlayer(PlayerEntity owner, int targetId, int areaId, Vector3 startPoint, HitData data)
    {
        List<PlayerEntity> result = GetHitResult(owner, targetId, areaId, startPoint, data);
        return result.Count > 0;
    }

    //命中检测
    private List<PlayerEntity> GetHitResult(PlayerEntity owner, int targetId, int areaId, Vector3 startPoint, HitData data)
    {
        Config_HitArea areaConfig = Config<Config_HitArea>.Get("HitArea", areaId);
        List<PlayerEntity> entitys = Module.Target.FindTarget(owner, targetId);
        List<PlayerEntity> result = new List<PlayerEntity>();

        //根据找到的目标进一步判断
        for (int i = 0; i < entitys.Count; i++)
        {
            PlayerEntity entity = entitys[i];
            bool isFind = false;
            if (areaConfig.areaType == AreaType.Circle)
            {
                isFind = CheckByCircle(entity.Entity.transform, startPoint, areaConfig.value[0]);
            }

            if (isFind)
            {
                result.Add(entity);
            }
        }

        return result;
    }

    private bool CheckByCircle(Transform findEntity, Vector3 startPoint, float radius)
    {
        return Vector3.Distance(findEntity.transform.position, startPoint) <= radius;
    }
}
