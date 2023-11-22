
using GameFramework.Fsm;
using GameFramework.Procedure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
	启动流程
*/
public class StartProcedure : ProcedureBase
{
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        Debug.Log("启动流程");
        //初始化语言，初始化资源
        //涉及到的资源都是build-in资源
        ChangeState<SplashProcedure>(procedureOwner);
    }
}
