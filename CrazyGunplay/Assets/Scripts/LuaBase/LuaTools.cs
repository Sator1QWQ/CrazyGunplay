using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class LuaTools
{
    /// <summary>
    /// 每次get的时候都会GetComponent，所以只能在初始化界面的时候调用
    /// </summary>
    /// <param name="goName">gameObject名字</param>
    /// <returns></returns>
    public static Component Get(GameObject go, List<GameObject> uiList, Dictionary<string, GameObject> uiDic, string goName)
    {
        Type type = GetCompType(goName);
        if(type.Name.Equals("GameObject"))
        {
            Debug.LogError("不允许获取GameObject类型! 使用GetGameObject函数");
            return null;
        }

        if (type == null)
        {
            Debug.LogError("组件类型不正确==" + goName + ", 界面为==" + go.name);
            return null;
        }

        if (uiDic.ContainsKey(goName))
        {
            return uiDic[goName].GetComponent(type);
        }

        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name.Equals(goName))
            {
                //缓存gameObject时需要小心物体被销毁的情况
                uiDic.Add(goName, uiList[i]);
                return uiList[i].GetComponent(type);
            }
        }

        Debug.LogError("未获取到gameObject==" + goName + ", 界面为==" + go.name);
        return null;
    }

    /// <summary>
    /// 获取GameObject类型
    /// </summary>
    /// <param name="go"></param>
    /// <param name="uiList"></param>
    /// <param name="uiDic"></param>
    /// <param name="goName"></param>
    /// <returns></returns>
    public static GameObject GetGameObject(GameObject go, List<GameObject> uiList, Dictionary<string, GameObject> uiDic, string goName)
    {
        if (uiDic.ContainsKey(goName))
        {
            return uiDic[goName].gameObject;
        }

        for (int i = 0; i < uiList.Count; i++)
        {
            if (uiList[i].name.Equals(goName))
            {
                //缓存gameObject时需要小心物体被销毁的情况
                uiDic.Add(goName, uiList[i]);
                return uiList[i].gameObject;
            }
        }

        Debug.LogError("未获取到gameObject==" + goName + ", 界面为==" + go.name);
        return null;
    }

    //获取组件类型
    private static Type GetCompType(string goName)
    {
        Type type;
        string exName = goName.Substring(goName.IndexOf("_") + 1);  //组件名
        switch (exName)
        {
            case "btn":
                type = typeof(Button);
                break;
            case "text":
                type = typeof(Text);
                break;
            case "sld":
                type = typeof(Slider);
                break;
            case "img":
                type = typeof(Image);
                break;
            case "eve":
                type = typeof(EventTrigger);
                break;
            case "go":
                type = typeof(GameObject);
                break;
            case "luaB":
                type = typeof(LuaBehaviour);
                break;
            case "grid":
                type = typeof(LuaGrid);
                break;
            default:
                type = null;
                break;
        }
        return type;
    }
}
