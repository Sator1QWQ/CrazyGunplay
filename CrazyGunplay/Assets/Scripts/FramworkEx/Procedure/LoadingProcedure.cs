using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using GameFramework.Procedure;
using GameFramework.Fsm;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class LoadingProcedure : ProcedureBase
{
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        //显示加载界面
        //Module.UI.OpenUIForm()
    }
}
