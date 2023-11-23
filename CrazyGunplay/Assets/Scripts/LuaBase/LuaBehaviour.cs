using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;

/*
* 作者：
* 日期：
* 描述：
		Lua生命周期
*/
public class LuaBehaviour : MonoBehaviour
{
    public TextAsset luaScript;

    private void Awake()
    {
        Module.Lua.env.DoString(luaScript.text);
    }

    private void Start()
    {
        
    }
}
