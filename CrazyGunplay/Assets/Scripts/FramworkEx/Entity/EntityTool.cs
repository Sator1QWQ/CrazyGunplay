using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		获取实体id用的
*/
public class EntityTool
{
    private static int characterEntityId = 0;
    private static int weaponEntityId = 100;
    private static int bulletEntityId = 1000;
    private static int summonEntityId = 2000;

    /// <summary>
    /// 角色实体id
    /// </summary>
    /// <returns></returns>
	public static int GetCharacterEntityId()
    {
        characterEntityId++;
        return characterEntityId;
    }

    /// <summary>
    /// 武器实体id
    /// </summary>
    /// <returns></returns>
    public static int GetWeaponEntityId()
    {
        weaponEntityId++;
        return weaponEntityId;
    }

    /// <summary>
    /// 获取子弹实体id
    /// </summary>
    /// <returns></returns>
    public static int GetBulletEntityId()
    {
        //子弹同时最多只能出现1000个
        if (bulletEntityId >= 2000)
        {
            bulletEntityId = 1000;
        }
        bulletEntityId++;
        return bulletEntityId;
    }

    /// <summary>
    /// 获取召唤物id
    /// </summary>
    /// <returns></returns>
    public static int GetSummonEntityId()
    {
        if(summonEntityId >= 3000)
        {
            summonEntityId = 2000;
        }
        summonEntityId++;
        return summonEntityId;
    }
}
