using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;
using System;

/*
* 作者：
* 日期：
* 描述：
		Lua生命周期
*/
public class LuaBehaviour : MonoBehaviour
{
    public LuaTable Table { get; private set; }  //require返回的表
    public string luaScriptPath;    //require的内容

    private void Awake()
    {
        byte[] bts = Module.Lua.GetByteAndLoad(luaScriptPath);

        //每次awake的时候重新加载lua
        object[] obj = Module.Lua.Env.DoString(bts);
        Table = obj[0] as LuaTable;
        Table.Get<Action>("Awake")?.Invoke();
    }

    private void Start()
    {
        Table.Get<Action>("Start")?.Invoke();
    }

    private void Update()
    {
        Table.Get<Action>("Update")?.Invoke();
    }

    private void OnDestroy()
    {
        Table.Get<Action>("OnDestroy")?.Invoke();
    }
}
