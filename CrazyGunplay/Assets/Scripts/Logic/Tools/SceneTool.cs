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
        string sceneName = Config.Get<string>("Scene", sceneId, "assetPath");

        //卸载ui
        Module.UI.CloseAllLoadingUIForms();
        Module.UI.CloseAllLoadedUIForms();

        //卸载实体
        Module.Entity.HideAllLoadingEntities();
        Module.Entity.HideAllLoadedEntities();

        //卸载场景
        string[] loadedScene = Module.Scene.GetUnloadingSceneAssetNames();
        for(int i = 0; i < loadedScene.Length; i++)
        {
            Module.Scene.UnloadScene(loadedScene[i]);
        }

        string path = GlobalDefine.SCENE_PATH + sceneName;
        Module.Scene.LoadScene(path);
    }
}
