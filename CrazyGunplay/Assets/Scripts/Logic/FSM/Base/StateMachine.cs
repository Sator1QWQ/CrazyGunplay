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
public class StateMachine<T> where T : class
{
	//分层状态，外部字典是状态层级集合，里面的字典是状态集合
	private Dictionary<StateLayer, Dictionary<StateType, StateBase<T>>> mStateDic = new Dictionary<StateLayer, Dictionary<StateType, StateBase<T>>>();
	private Dictionary<StateLayer, StateBase<T>> mCurStateDic = new Dictionary<StateLayer, StateBase<T>>();   //每层当前状态
	private T mOwner;

    public StateMachine(T owner)
    {
        mOwner = owner;
    }

	public void AddState(StateLayer layer, StateBase<T> state)
    {
		if(!mStateDic.ContainsKey(layer))
        {
			mStateDic.Add(layer, new Dictionary<StateType, StateBase<T>>());
			mCurStateDic.Add(layer, state);
		}
		state.Init(layer);
		mStateDic[layer].Add(state.Type, state);
    }

	/// <summary>
	/// 转换状态
	/// </summary>
	/// <param name="layer">状态层级</param>
	/// <param name="type">状态id</param>
	public void ChangeState(StateLayer layer, StateType type)
    {
		if(!mStateDic[layer].ContainsKey(type))
        {
			Debug.LogError("不存在此状态！layer==" + layer + ", type==" + type);
			return;
        }

		StateBase<T> lastState = mCurStateDic[layer];
		StateBase<T> changeState = mStateDic[layer][type];
		mCurStateDic[layer].OnExit(mOwner);
		mCurStateDic[layer] = changeState;
		changeState.OnEnter(mOwner);

		ChangeStateEventArgs<T> args = ChangeStateEventArgs<T>.Create(mOwner, layer, lastState.Type, changeState.Type);
		Module.Event.FireNow(null, args);
    }

	public StateBase<T> GetState(StateLayer layer, StateType type)
    {
		return mStateDic[layer][type];
    }

	/// <summary>
	/// 每帧调用
	/// </summary>
	public void OnUpdate()
    {
		foreach(KeyValuePair<StateLayer, StateBase<T>> pair in mCurStateDic)
        {
			//true的话，下一帧再执行
			if(pair.Value.OnExecute(mOwner))
            {
				break;
			}
			Debug.Log($"当前层=={pair.Key}, 当前状态=={pair.Value}");
		}
	}
}
