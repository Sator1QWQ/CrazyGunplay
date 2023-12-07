using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using System;

/*
* 作者：
* 日期：
* 描述：
		所有Item需要挂载这个脚本
*/
[RequireComponent(typeof(LuaBehaviour))]
public class LuaItem : MonoBehaviour
{
    public List<GameObject> uiList;
    [HideInInspector] public LuaBehaviour behaviour;
    [HideInInspector] public bool isSelect = false;

    private Dictionary<string, GameObject> uiDic = new Dictionary<string, GameObject>();
    private int index;

    private void Start()
    {
        behaviour = GetComponent<LuaBehaviour>();
        Action<LuaItem, int> itemInit = behaviour.Table.Get<Action<LuaItem, int>>("ItemInit");
        itemInit?.Invoke(this, index);
    }

    /// <summary>
    /// 每次get的时候都会GetComponent，所以只能在初始化界面的时候调用
    /// </summary>
    /// <param name="goName">gameObject名字</param>
    /// <returns></returns>
    public Component Get(string goName) => LuaTools.Get(gameObject, uiList, uiDic, goName);

    /// <summary>
    /// 获取GameObject
    /// </summary>
    /// <param name="goName"></param>
    /// <returns></returns>
    public GameObject GetGameObject(string goName) => LuaTools.GetGameObject(gameObject, uiList, uiDic, goName);

    public void SetIndex(int index)
    {
        this.index = index;
    }

    /// <summary>
    /// 被选中或者取消选中时调用
    /// </summary>
    /// <param name="b"></param>
    public void SelectChange()
    {
        isSelect = !isSelect;
        Action<bool> act = behaviour.Table.Get<Action<bool>>("OnSelect");
        act?.Invoke(isSelect);
    }
}
