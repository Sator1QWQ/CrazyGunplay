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
        Debug.Log("游戏流程");
        //进入主界面
        //Module.UI.OpenUIForm("Assets/Resource/UI/StartPanel.prefab", "NormalGroup");
        //Module.Scene.LoadScene("Assets/Resource/Scenes/Battle_1.unity");
        
        Module.Entity.AddEntityGroup("normal", 1, 10, 10, 1);
        Module.Entity.ShowEntity(123, typeof(PlayerEntity), "Assets/Resource/Models/Player/Player.prefab", "normal", new int[]{ 1, 1001 });
        //Module.Entity.ShowEntity(124, typeof(PlayerEntity), "Assets/Resource/Models/Player/Player.prefab", "normal", new int[] { 2, 1002 });
    }
}
