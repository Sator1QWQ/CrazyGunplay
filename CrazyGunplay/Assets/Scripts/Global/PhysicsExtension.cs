using GameFramework.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 射线检测扩展
/// </summary>
public class PhysicsExtension
{
    /// <summary>
    /// 是否检测到目标
    /// 比如目标类型为同队，那么只检测同队的玩家，其他玩家为false
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="hitInfo"></param>
    /// <param name="maxDistance"></param>
    /// <param name="layerMask"></param>
    /// <param name="ownerPlayer"></param>
    /// <param name="targetId"></param>
    /// <returns></returns>
    public static bool RayCastHitPlayer(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, int ownerPlayer, int targetId)
    {
        if(Physics.Raycast(origin, direction, out hitInfo, maxDistance, layerMask))
        {
            PlayerEntity player = hitInfo.transform.GetComponent<PlayerEntity>();
            if(player != null)
            {
                bool isTarget = Module.Team.IsPlayerIsTarget(ownerPlayer, player.PlayerId, targetId);
                return isTarget;
            }
            return true;
        }
        return false;
    }
}
