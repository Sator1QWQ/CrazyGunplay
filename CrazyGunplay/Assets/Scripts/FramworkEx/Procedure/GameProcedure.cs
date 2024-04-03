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
    private bool testMode = true;

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        Debug.Log("游戏流程");

        if(!testMode)
        {
            //进入主界面
            Module.UI.OpenUIForm("Assets/Resource/UI/StartPanel.prefab", "NormalGroup");
        }
        else
        {
            //战斗测试
            Module.Scene.LoadScene("Assets/Resource/Scenes/Battle_1.unity");
            LuaBehaviour be = new GameObject().AddComponent<LuaBehaviour>();
            be.luaScriptPath = "Test/Test";
            be.Init();
            
            //Module.Entity.AddEntityGroup("normal", 1, 10, 10, 1);

            //int heroId = 1001;
            //int heroId2 = 1002;
            //string asset = Config.Get<string>("Character", heroId, "model");
            //string asset2 = Config.Get<string>("Character", heroId2, "model");
            //Module.Entity.ShowEntity(123, typeof(PlayerEntity), GlobalDefine.PLAYER_MODEL_PATH + asset + ".prefab", "normal", new int[] { 1, heroId });
            //Module.Entity.ShowEntity(124, typeof(PlayerEntity), GlobalDefine.PLAYER_MODEL_PATH + asset2 + ".prefab", "normal", new int[] { 2, heroId2 });
        }
    }
}
