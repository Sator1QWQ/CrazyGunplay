using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		所有组件注册
*/
public sealed class Module : MonoBehaviour
{
    public static BaseComponent Base { get; private set; }

    /// <summary>
    /// 实体
    /// </summary>
    public static EntityComponent Entity { get; private set; }

    /// <summary>
    /// 事件
    /// </summary>
    public static EventComponent Event { get; private set; }

    /// <summary>
    /// 界面
    /// </summary>
    public static UIComponent UI { get; private set; }

    /// <summary>
    /// 资源加载
    /// </summary>
    public static ResourceComponent Resource { get; private set; }

    /// <summary>
    /// lua组件
    /// </summary>
    public static LuaComponent Lua { get; private set; }

    /// <summary>
    /// 场景组件
    /// </summary>
    public static SceneComponent Scene { get; private set; }

    private void Start()
    {
        InitModuleComponent();
    }

    //初始化模块
    private void InitModuleComponent()
    {

        Base = GameEntry.GetComponent<BaseComponent>();
        Entity = GameEntry.GetComponent<EntityComponent>();
        Event = GameEntry.GetComponent<EventComponent>();
        UI = GameEntry.GetComponent<UIComponent>();
        Resource = GameEntry.GetComponent<ResourceComponent>();
        Lua = GameEntry.GetComponent<LuaComponent>();
        Scene = GameEntry.GetComponent<SceneComponent>();
        Debug.Log("模块初始化完成");
    }
}
