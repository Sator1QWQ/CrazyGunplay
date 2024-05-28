using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		控制行为基类
*/
public abstract class ControlActionBase
{
	/// <summary>
	/// 是否暂停
	/// </summary>
	public bool IsPause { get; set; }

	/// <summary>
	/// 控制器类型
	/// </summary>
	public abstract ControllerType CtrlType { get; }

	public abstract void DoAction(PlayerController controller);
}
