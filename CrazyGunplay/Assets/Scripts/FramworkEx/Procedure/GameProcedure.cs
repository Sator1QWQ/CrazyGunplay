using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using GameFramework.Fsm;
using GameFramework.Resource;

/*
* 作者：
* 日期：
* 描述：
		游戏流程，这时进入游戏
*/
public class GameProcedure : ProcedureBase
{
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        LoadAssetCallbacks callBack = new LoadAssetCallbacks((string assetName, object asset, float duration, object userData) =>
        {
            Debug.Log("资源加载完成==" + assetName + ", obj==" + asset + "dura==" + duration);
            Object.Instantiate(asset as GameObject);
        });
        //进入主界面
        //Module.Resource.LoadAsset("Assets/Resource/UI/StartPanel.prefab", callBack);
        Module.UI.OpenUIForm("Assets/Resource/UI/StartPanel.prefab", "NormalGroup");
    }
}
