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
	public Component Get(string goName) => LuaTools.Get(gameObject, uiList, uiDic, goName);

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        //只有在Init时才把panel传给lua，其他周期函数不传，在Init做好缓存
        transform.localPosition = Vector3.zero;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(GlobalDefine.DESIGN_WIDTH, GlobalDefine.DESIGN_HEIGHT);
        behaviour.Table.Get<Action<LuaPanel>>("OnInit")?.Invoke(this);
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        behaviour.Table.Get<Action>("OnOpen")?.Invoke();
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
        base.OnClose(isShutdown, userData);
        behaviour.Table.Get<Action<LuaPanel>>("OnClose")?.Invoke(this);
    }

    public void Close()
    {
        Module.UI.CloseUIForm(UIForm.SerialId);
    }
}
