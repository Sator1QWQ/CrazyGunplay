﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;
using System.IO;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class LuaComponent : GameFrameworkComponent
{
	public static LuaEnv env;

    protected override void Awake()
    {
        base.Awake();
        env = new LuaEnv();
        env.AddLoader(LoadFile);
        env.DoString("require 'Main'");
    }

    //加载Lua文件
    private byte[] LoadFile(ref string path)
    {
        string newPath = path.Replace(".", "/");

        FileStream file = new FileStream(path, FileMode.Open);
        byte[] bts = new byte[file.Length];
        file.Read(bts, 0, bts.Length);
        return bts;
    }
}
