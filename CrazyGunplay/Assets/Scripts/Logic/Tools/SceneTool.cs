using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using XLua;

/// <summary>
/// 场景工具
/// </summary>
[LuaCallCSharp]
public class SceneTool
{
    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="sceneId"></param>
    public static void ChangeScene(int sceneId)
    {
        string sceneName = Config<Config_Scene>.Get("Scene", sceneId).assetPath;
        RemoveSceneAssets();
        string path = GlobalDefine.SCENE_PATH + sceneName;
        Module.Scene.LoadScene(path);
    }

    /// <summary>
    /// 除了Start场景，其他的场景都卸载
    /// </summary>
    public static void UnloadAllScene()
    {
        //卸载场景
        string[] loadedScene = Module.Scene.GetLoadedSceneAssetNames();
        for (int i = 0; i < loadedScene.Length; i++)
        {
            Module.Scene.UnloadScene(loadedScene[i]);
        }

        //卸载ui
        Module.UI.CloseAllLoadingUIForms();
        Module.UI.CloseAllLoadedUIForms();

        //卸载实体
        Module.Entity.HideAllLoadingEntities();
        Module.Entity.HideAllLoadedEntities();

        Module.Timer.RemoveAllTimer();
    }

    //清除场景资源
    private static void RemoveSceneAssets()
    {
        //卸载ui
        Module.UI.CloseAllLoadingUIForms();
        Module.UI.CloseAllLoadedUIForms();

        //卸载实体
        Module.Entity.HideAllLoadingEntities();
        Module.Entity.HideAllLoadedEntities();

        Module.Timer.RemoveAllTimer();

        //卸载场景
        string[] loadedScene = Module.Scene.GetUnloadingSceneAssetNames();
        for (int i = 0; i < loadedScene.Length; i++)
        {
            Module.Scene.UnloadScene(loadedScene[i]);
        }
    }
}
