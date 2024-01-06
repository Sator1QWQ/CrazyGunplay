using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		分层状态机
*/
public class StateMachine
{
	//分层状态，外部字典是状态层级集合，里面的字典是状态集合
	private Dictionary<StateLayer, Dictionary<StateType, StateBase>> mStateDic = new Dictionary<StateLayer, Dictionary<StateType, StateBase>>();
	private Dictionary<StateLayer, StateBase> mCurStateDic = new Dictionary<StateLayer, StateBase>();	//每层当前状态

	public void AddState(StateLayer layer, StateBase state)
    {
		if(!mStateDic.ContainsKey(layer))
        {
			mStateDic.Add(layer, new Dictionary<StateType, StateBase>());
			mCurStateDic.Add(layer, state);
		}
		mStateDic[layer].Add(state.Type, state);
    }

	public void ChangeState(StateLayer layer, StateBase changeState)
    {
		if(!mStateDic[layer].ContainsKey(changeState.Type))
        {
			Debug.LogError("不存在此状态！layer==" + layer + ", type==" + changeState.Type);
			return;
        }

		mCurStateDic[layer].OnExit();
		mCurStateDic[layer] = changeState;
		changeState.OnEnter();
    }

	/// <summary>
	/// 每帧调用
	/// </summary>
	public void OnUpdate()
    {
		foreach(KeyValuePair<StateLayer, StateBase> pair in mCurStateDic)
        {
			pair.Value.OnExecute();
        }
    }
}
