using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class PlayerBattleData
{
    private int playerId;
    public int PlayerId { get => playerId; set => playerId = value; }

    private int heroId;
    public int HeroId { get => heroId; set { heroId = value; } }

    private int weaponId;
    public int WeaponId { get => weaponId; set { weaponId = value; } }

    private int life;   //生命数
    public int Life { get => life; set { life = value; } }
}

/// <summary>
/// 玩家数据 由lua端同步
/// </summary>
public class PlayerDataComponent : GameFrameworkComponent
{
    private Dictionary<int, PlayerBattleData> dataDic = new Dictionary<int, PlayerBattleData>();
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
