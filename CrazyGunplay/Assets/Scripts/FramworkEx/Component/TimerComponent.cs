using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using System;

/*
* 作者：
* 日期：
* 描述：
		基于unity的定时器
*/
public class TimerComponent : GameFrameworkComponent
{
    public class TimerData
    {
        public float startTime;
        public float delay;
        public float duration;
        public float continueTime;  //计时开始后经过了多长时间
        public float remainTime;    //剩余时间
        public Action<TimerData> act;
        public Action<TimerData> endAct;
        public object userdata;
    }

    private List<TimerData> mTimerList = new List<TimerData>();

    public TimerData AddTimer(Action<TimerData> act, float delayTime)
    {
        TimerData data = new TimerData();
        data.startTime = Time.time;
        data.delay = delayTime;
        data.duration = 0;
        data.act = act;
        mTimerList.Add(data);
        return data;
    }

    /// <summary>
    /// 添加定时器
    /// </summary>
    /// <param name="act">每帧调用函数</param>
    /// <param name="endAct">结束函数</param>
    /// <param name="delayTime">延迟时间 单位：秒</param>
    /// <param name="duration">持续时间 单位：秒</param>
    public TimerData AddUpdateTimer(Action<TimerData> act, Action<TimerData> endAct, float delayTime = 0, float duration = 0)
    {
        TimerData data = new TimerData();
        data.startTime = Time.time;
        data.delay = delayTime;
        data.duration = duration;
        data.act = act;
        data.endAct = endAct;
        mTimerList.Add(data);
        return data;
    }


    /// <summary>
    /// 外部可能需要手动移除定时器
    /// </summary>
    /// <param name="data"></param>
    public void RemoveTimer(TimerData data)
    {
        if(mTimerList.Contains(data))
        {
            mTimerList.Remove(data);
        }
    }

    private void Update()
    {
        for(int i = 0; i < mTimerList.Count; i++)
        {
            TimerData data = mTimerList[i];
            //定时器开始
            if(Time.time >= data.startTime + data.delay)
            {
                data.continueTime += Time.deltaTime;
                data.remainTime = data.duration - data.continueTime;
                //持续时间为-1，则永远不会结束，需要外部手动结束
                if (data.duration == -1)
                {
                    data.act(data);
                    continue;
                }

                //持续时间结束
                if(Time.time >= data.startTime + data.duration + data.delay)
                {
                    //持续时间为0时，会直接remove，这里需要手动调一次act
                    if(data.duration == 0)
                    {
                        data.act(data);
                    }
                    data.endAct?.Invoke(data);
                    mTimerList.RemoveAt(i);
                    i--;
                }
                else
                {
                    data.act(data);
                }
            }
        }
    }

    /// <summary>
    /// 强制移除所有定时器
    /// </summary>
    public void RemoveAllTimer()
    {
        mTimerList.Clear();
    }
}
