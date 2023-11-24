using System.Collections;
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
	public LuaEnv Env { get; private set; }

    //缓存了每个lua文件
    private Dictionary<string, byte[]> fileDic = new Dictionary<string, byte[]>();

    protected override void Awake()
    {
        base.Awake();
        Env = new LuaEnv();
        Env.AddLoader(LoadFile);
        Env.DoString("require 'Main'");
    }

    //加载Lua文件
    public byte[] LoadFile(ref string path)
    {
        string replace = path.Replace(".", "/");
        string filePath = GlobalDefine.LUA_PATH + replace + ".lua";
        byte[] result;

        //需要字典缓存
        if(fileDic.ContainsKey(path))
        {
            result = fileDic[path];
        }
        else
        {
            FileStream file = new FileStream(filePath, FileMode.Open);
            byte[] bts = new byte[file.Length];
            file.Read(bts, 0, bts.Length);
            file.Dispose();
            result = bts;
        }

        return result;
    }

    /// <summary>
    /// 根据文件获取byte
    /// </summary>
    /// <param name="path">require的路径</param>
    /// <returns></returns>
    public byte[] GetByte(string path)
    {
        if(fileDic.ContainsKey(path))
        {
            return fileDic[path];
        }

        return null;
    }

    /// <summary>
    /// 根据文件获取byte，如果没加载就自动加载
    /// </summary>
    /// <param name="path">require的路径</param>
    /// <returns></returns>
    public byte[] GetByteAndLoad(string path)
    {
        byte[] bts = GetByte(path);
        if (bts == null)
        {
            bts = LoadFile(ref path);
        }
        return bts;
    }

    /// <summary>
    /// require lua脚本
    /// </summary>
    /// <param name="path"></param>
    public void Require(string path)
    {
        Env.DoString($"require '{path}'");
    }
}
