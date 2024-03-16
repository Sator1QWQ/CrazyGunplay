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
    public LuaBehaviour parent; //引用其他LuaBehaviour

    private Action update;
    private Action onDestroy;

    private void Awake()
    {
        byte[] bts = Module.Lua.GetByteAndLoad(luaScriptPath);

        //每次awake的时候重新加载lua
        string scriptName = luaScriptPath.Substring(luaScriptPath.LastIndexOf('/') + 1);
        string replace = luaScriptPath.Replace('/', '.');
        string requirePath = "require '" + replace + "'";
        object[] obj = Module.Lua.Env.DoString("return " + requirePath, scriptName);
        Table = obj[0] as LuaTable;
        if(parent != null)
        {
            Table.Set("_Parent", parent);
        }
        Table.Get<Action>("Awake")?.Invoke();
        update = Table.Get<Action>("Update");
        onDestroy = Table.Get<Action>("OnDestroy");
    }

    private void OnEnable()
    {
        Table.Get<Action>("OnEnable")?.Invoke();
    }

    private void OnDisable()
    {
        Table.Get<Action>("OnDisable")?.Invoke();
    }

    private void Start()
    {
        Table.Get<Action>("Start")?.Invoke();
    }

    private void Update()
    {
        update?.Invoke();
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke();
    }
}
