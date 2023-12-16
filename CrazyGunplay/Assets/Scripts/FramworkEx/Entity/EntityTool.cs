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
}
