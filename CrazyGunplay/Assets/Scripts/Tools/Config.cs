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
		配置表
*/
public class Config
{
    private static Dictionary<string, LuaTable> configDic = new Dictionary<string, LuaTable>();

    /// <summary>
    /// 获取某个配置表
    /// </summary>
    /// <param name="configName"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static LuaTable Get(string configName, int id)
    {
        if(configDic.ContainsKey(configName))
        {
            return configDic[configName].Get<int, LuaTable>(id);
        }

        Module.Lua.Env.DoString($"require 'Configs.Config.{configName}'");
        LuaTable table = Module.Lua.Env.Global.Get<LuaTable>(configName);
        configDic.Add(configName, table);  //小心内存释放问题
        LuaTable config = table.Get<int, LuaTable>(id);
        if(config == null)
        {
            Debug.Log("找不到数据！检查表是否有问题 configName==" + configName + ", id==" + id);
            return null;
        }
        return config;
    }

    /// <summary>
    /// 获取配置表的某个数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configName"></param>
    /// <param name="id"></param>
    /// <param name="key"></param>
    /// <returns></returns>
	public static T Get<T>(string configName, int id, string key)
    {
        LuaTable config = Get(configName, id);
        T t = config.Get<T>(key);
        return t;
    }
}
