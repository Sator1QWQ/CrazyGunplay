using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/*
* 作者：
* 日期：
* 描述：
		每个Lua界面都需要挂载这个脚本
*/
[RequireComponent(typeof(LuaBehaviour))]
public class LuaPanel : UIFormLogic
{
	public List<GameObject> uiList;
	private Dictionary<string, GameObject> uiDic = new Dictionary<string, GameObject>();
    private LuaBehaviour behaviour;

    private void Awake()
    {
        behaviour = GetComponent<LuaBehaviour>();
    }

    /// <summary>
    /// 每次get的时候都会GetComponent，所以只能在初始化界面的时候调用
    /// </summary>
    /// <param name="goName">gameObject名字</param>
    /// <param name="type">组件类型</param>
    /// <returns></returns>
	public Component Get(string goName, System.Type type)
    {
		if(uiDic.ContainsKey(goName))
        {
			return uiDic[goName].GetComponent(type);
        }

		for(int i = 0; i < uiList.Count; i++)
        {
			if(uiList[i].name.Equals(goName))
            {
                //缓存gameObject时需要小心物体被销毁的情况
                uiDic.Add(goName, uiList[i]);
                return uiList[i].GetComponent(type);
            }
        }

        return null;
    }

    protected override void OnInit(object userData)
    {
        Action<object> act = behaviour.Table.Get<Action<object>>("OnInit");
        act?.Invoke(userData);
    }
}
