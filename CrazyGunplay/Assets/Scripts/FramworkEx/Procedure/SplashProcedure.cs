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
		闪屏流程，显示logo
*/
public class SplashProcedure : ProcedureBase
{
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        Debug.Log("进入闪屏流程");


#if UNITY_EDITOR
        //预加载流程
        //ChangeState<>
#else
        //进入验证版本
#endif
    }
}
