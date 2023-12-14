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

    private void Awake()
    {
        byte[] bts = Module.Lua.GetByteAndLoad(luaScriptPath);

        //每次awake的时候重新加载lua
        string scriptName = luaScriptPath.Substring(luaScriptPath.LastIndexOf('/') + 1);
        object[] obj = Module.Lua.Env.DoString(bts, scriptName);
        Table = obj[0] as LuaTable;
        if(parent != null)
        {
            Table.Set("_Parent", parent);
        }
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

    public void Call(string func)
    {
        Action act = Table.Get<Action>(func);
        if(act == null)
        {
            Debug.LogError("lua 函数" + func + "为空！ gameObject==" + gameObject.name);
            return;
        }
        act();
    }
}
