using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		状态基类
*/
public abstract class StateBase
{
	/// <summary>
	/// 状态类型
	/// </summary>
	public abstract StateType Type { get; }
	
	/// <summary>
	/// 所处的层级
	/// </summary>
	public StateLayer Layer { get; private set; }

	/// <summary>
	/// new完后必须初始化
	/// </summary>
	/// <param name="layer"></param>
	/// <param name="type"></param>
	public void Init(StateLayer layer)
    {
		Layer = layer;
	}

	/// <summary>
	/// 进入时
	/// </summary>
	public virtual void OnEnter() { }

	/// <summary>
	/// 每帧调用
	/// </summary>
	public virtual void OnExecute() { }

	/// <summary>
	/// 离开时
	/// </summary>
	public virtual void OnExit() { }
}
