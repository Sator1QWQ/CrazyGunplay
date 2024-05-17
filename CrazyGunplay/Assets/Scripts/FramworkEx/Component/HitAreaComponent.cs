using GameFramework.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class HitAreaComponent : GameFrameworkComponent
{
    /// <summary>
    /// 命中玩家判断，不一定是伤害命中，也可以是其他的回血等命中
    /// </summary>
    /// <param name="owner">玩家</param>
    /// <param name="targetId">目标表id</param>
    /// <param name="areaId">范围表id</param>
    /// <param name="startPoint"></param>
    public void HitPlayerAction(PlayerEntity owner, int targetId, int areaId, Vector3 startPoint)
    {
        Config_HitArea areaConfig = Config<Config_HitArea>.Get("HitArea", areaId);
        List<PlayerEntity> entitys = Module.Target.FindTarget(owner, targetId);
        List<PlayerEntity> result = new List<PlayerEntity>();
        
        //根据找到的目标进一步判断
        for(int i = 0; i < entitys.Count; i++)
        {
            PlayerEntity entity = entitys[i];
            bool isFind = false;
            if(areaConfig.areaType == AreaType.Circle)
            {
                isFind = CheckByCircle(entity.Entity.transform, startPoint, areaConfig.value[0]);
            }
            
            if(isFind)
            {
                result.Add(entity);
            }
        }


    }

    private bool CheckByCircle(Transform findEntity, Vector3 startPoint, float radius)
    {
        return Vector3.Distance(findEntity.transform.position, startPoint) <= radius;
    }
}
