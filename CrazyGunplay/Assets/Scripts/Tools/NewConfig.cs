using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewConfig<T> where T : class, IConfigBase
{
    public static Dictionary<string, Dictionary<int, T>> configDic = new Dictionary<string, Dictionary<int, T>>();

    /// <summary>
    /// 获取整张配置表
    /// </summary>
    /// <param name="configName"></param>
    /// <returns></returns>
    public static Dictionary<int, T> Get(string configName)
    {
        if (!configDic.ContainsKey(configName))
        {
            Dictionary<int, T> cfg = Module.Lua.Env.Global.Get<Dictionary<int, T>>(configName);
            if(cfg == null)
            {
                Module.Lua.Env.DoString($"require 'Configs.Config.{configName}'");
                cfg = Module.Lua.Env.Global.Get<Dictionary<int, T>>(configName);
            }
            configDic.Add(configName, cfg);
            return cfg;
        }

        return configDic[configName];
    }

    /// <summary>
    /// 获取配置表的某行数据
    /// </summary>
    /// <param name="configName"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static T Get(string configName, int id)
    {
        Dictionary<int, T> cfg = Get(configName);
        if (!cfg.ContainsKey(id))
        {
            Debug.LogError($"表{configName}中不存在id为{id}！！");
            return null;
        }
        return cfg[id];
    }
}
