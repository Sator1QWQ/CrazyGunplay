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
public abstract class StateBase<T> where T : class
{
	/// <summary>
	/// 状态类型
	/// </summary>
	public abstract StateType Type { get; }
	
	/// <summary>
	/// 所处的层级
	/// </summary>
	public StateLayer Layer { get; private set; }

	public void Init(StateLayer layer)
    {
		Layer = layer;
    }

	/// <summary>
	/// 进入时
	/// </summary>
	public virtual void OnEnter(T owner) { }

	/// <summary>
	/// 每帧调用 返回true：状态改变，下一帧再操作状态机	fasle：状态未改变
	/// 状态改变但是没有返回true时，会报错
	/// </summary>
	public virtual bool OnExecute(T owner) => false;

	/// <summary>
	/// 离开时
	/// </summary>
	public virtual void OnExit(T owner) { }
}
