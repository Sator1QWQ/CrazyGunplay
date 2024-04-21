using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : IReference
{
    private BuffManager manager;
    private Config_Buff mConfig;
    private int mPlayerId;
    
    /// <summary>
    /// buff持续时间
    /// </summary>
    public float Duration { get; private set; }

    /// <summary>
    /// buff经过了多长时间
    /// </summary>
    public float RunTime { get; private set; }

    private string luaKey;   //lua buffData中修改哪个key 比如buff为移速，则key为moveSpeed

    /// <summary>
    /// buff数据 只能是float类型
    /// </summary>
    public float BuffValue { get; private set; }

    /// <summary>
    /// buff剩余时间
    /// </summary>
    public float RemainTime => Duration - RunTime;

    private TimerComponent.TimerData timerData;

    /// <summary>
    /// 每次添加buff，而不是Reset的时候都会初始化
    /// </summary>
    /// <param name="buffId"></param>
    /// <param name="duration"></param>
    public void InitData(BuffManager manager, int player, int buffId, float duration, float buffValue)
    {
        //对象池重新获取时不需要再读取数据
        if(mConfig == null)
        {
            mConfig = Config<Config_Buff>.Get("Buff", buffId);
        }
        Duration = duration;
        mPlayerId = player;
        BuffValue = buffValue;

        //BuffManager无法暂停定时器，但是可以终止定时器
        Module.Timer.AddUpdateTimer(OnTimerUpdate, OnTimerEnd, 0, duration);
        this.manager = manager;
    }

    /// <summary>
    /// 每次添加buff的时候会调用一次
    /// Reset不会调用
    /// </summary>
    public void OnBuffFirstStart()
    {
        switch (mConfig.type)
        {
            case BuffType.MoveSpeed:
                luaKey = "moveSpeedScale";
                break;
            case BuffType.GetBeatBack:
                luaKey = "getBeatScale";
                break;
            case BuffType.Hp:
                luaKey = "hpAdd";
                break;
            case BuffType.FireRate:
                luaKey = "fireRateScale";
                break;
            case BuffType.BeatBackValue:
                luaKey = "attackScale";
                break;
        }

        BuffStartEventArgs args = BuffStartEventArgs.Create(mPlayerId, luaKey, BuffValue);
        Module.Event.FireNow(this, args);
    }

    private void OnTimerUpdate(TimerComponent.TimerData data)
    {

    }

    private void OnTimerEnd(TimerComponent.TimerData data)
    {
        manager.RemoveBuff(mConfig.id);
        BuffEndEventArgs args = BuffEndEventArgs.Create(mPlayerId, luaKey);
        Module.Event.FireNow(this, args);
    }

    /// <summary>
    /// 重置buff时间
    /// </summary>
    /// <param name="duration"></param>
    public void ResetBuffTime(float duration)
    {
        Duration = duration;
        RunTime = 0;
        Module.Timer.RemoveTimer(timerData);
        timerData = Module.Timer.AddUpdateTimer(OnTimerUpdate, OnTimerEnd, 0, duration);
    }

    public void Clear()
    {
        Duration = 0;
        RunTime = 0;
        Module.Timer.RemoveTimer(timerData);
        timerData = null;
    }
}
