using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class PlayerBattleData
{
    public int PlayerId { get; set; }
    public int HeroId { get; set;}
    public int WeaponId { get; set; }
    /// <summary>
    /// 生命数
    /// </summary>
    public int Life { get; set; }

    /// <summary>
    /// 击退倍率，1为最低
    /// </summary>
    public float BeatBackValue { get; set; }
    
    /// <summary>
    /// 击退百分比
    /// </summary>
    public int BeatBackPercent { get; set; }
}

/// <summary>
/// 玩家数据 由lua端同步
/// </summary>
public class PlayerDataComponent : GameFrameworkComponent
{
    private Dictionary<int, PlayerBattleData> dataDic = new Dictionary<int, PlayerBattleData>();

    /// <summary>
    /// 战斗状态
    /// </summary>
    public BattleState State { get; private set; }

    private void Start()
    {
        Module.Event.Subscribe(SyncPlayerDataEventArgs.EventId, SyncPlayerData);
        Module.Event.Subscribe(BattleStateChangeArgs.EventId, BattleStateChange);
    }

    public PlayerBattleData GetData(int playerId)
    {
        return dataDic[playerId];
    }

    private void SyncPlayerData(object sender, GameEventArgs e)
    {
        SyncPlayerDataEventArgs args = e as SyncPlayerDataEventArgs;
        if (!dataDic.ContainsKey(args.Data.PlayerId))
        {
            PlayerBattleData d = new PlayerBattleData();
            d.PlayerId = args.Data.PlayerId;
            dataDic.Add(args.Data.PlayerId, d);
        }
        PlayerBattleData data = dataDic[args.Data.PlayerId];
        data.HeroId = args.Data.HeroId;
        data.WeaponId = args.Data.WeaponId;
        data.Life = args.Data.Life;
        data.BeatBackValue = args.Data.BeatBackValue;
        data.BeatBackPercent = args.Data.BeatBackPercent;
    }

    private void BattleStateChange(object sender, GameEventArgs e)
    {
        BattleStateChangeArgs args = e as BattleStateChangeArgs;
        State = args.State;
        Debug.Log("C# 战斗状态改变 状态==" + State);
    }

    private void OnDisable()
    {
        Module.Event.Unsubscribe(SyncPlayerDataEventArgs.EventId, SyncPlayerData);
    }
}
