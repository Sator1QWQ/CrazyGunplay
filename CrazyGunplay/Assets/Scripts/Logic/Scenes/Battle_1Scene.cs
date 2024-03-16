using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class Battle_1Scene : MonoBehaviour
{
    private void OnEnable()
    {
        LuaTable battleSetting;
        Module.Lua.Env.Global.Get<string, LuaTable>("MBattleSetting", out battleSetting);
        LuaTable instance;
        battleSetting.Get("Instance", out instance);
        float countDown = instance.Get<float>("countDown");
        Debug.Log("countDown==" + countDown);
        Module.Timer.AddUpdateTimer(data =>
        {
            countDown-=Time.deltaTime;
            UpdateTimer(data, countDown);
        },null, 0, -1);
    }

    private void UpdateTimer(TimerComponent.TimerData data, float countDown)
    {
        Debug.Log("CountDown==" + countDown);
        if(countDown <= 0)
        {
            Debug.Log("计时结束");
            Module.Timer.RemoveTimer(data);
        }
    }
}
