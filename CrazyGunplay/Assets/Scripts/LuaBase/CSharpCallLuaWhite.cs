using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using System;
using XLua;

/*
* 作者：
* 日期：
* 描述：
		xlua白名单
*/
public static class CSharpCallLuaWhite
{
    [CSharpCallLua]
    public static List<Type> whiteList = new List<Type>()
    {
        typeof(Action<LuaTable, LuaItem, int>),
        typeof(Action<LuaTable, bool>),
        typeof(Action<LuaTable, int>),
        typeof(Action<PlayerBattleData>),
    };
}
