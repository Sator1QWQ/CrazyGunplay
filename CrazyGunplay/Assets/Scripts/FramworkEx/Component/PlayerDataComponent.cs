using GameFramework.Entity;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using XLua;

[CSharpCallLua]
public interface PlayerBattleData
{
    int id { get; set; }
    int heroId { get; set;}
    int weaponId { get; set; }
    /// <summary>
    /// 生命数
    /// </summary>
    int life { get; set; }

    /// <summary>
    /// 击退倍率，1为最低
    /// </summary>
    float beatBackValue { get; set; }
    
    /// <summary>
    /// 击退百分比
    /// </summary>
    int beatBackPercent { get; set; }
}

/// <summary>
/// 由buff操作的数据
/// </summary>
[CSharpCallLua]
public interface BuffData
{
    /// <summary>
    /// 移速倍率
    /// </summary>
    float moveSpeedScale { get; set; }

    /// <summary>
    /// 受伤倍率
    /// </summary>
    float getBeatScale { get; set; }

    /// <summary>
    /// 血量增加数值 可以为负
    /// </summary>
    float hpAdd { get; set; }

    /// <summary>
    /// 射速倍率
    /// </summary>
    float fireRateScale { get; set; }

    /// <summary>
    /// 攻击力倍率
    /// </summary>
    float attackScale { get; set; }
}

/// <summary>
/// 玩家数据 由lua端同步
/// </summary>
public class PlayerDataComponent : GameFrameworkComponent
{
    private Dictionary<int, PlayerBattleData> dataDic = new Dictionary<int, PlayerBattleData>();
    private Dictionary<int, BuffData> buffDataDic = new Dictionary<int, BuffData>();

    /// <summary>
    /// 战斗状态
    /// </summary>
    public BattleState State { get; private set; }

    private void Start()
    {
        Module.Event.Subscribe(SyncPlayerDataEventArgs.EventId, SyncPlayerData);
        Module.Event.Subscribe(BattleStateChangeArgs.EventId, BattleStateChange);
        Module.Event.Subscribe(SyncBuffDataEventArgs.EventId, SyncPlayerBuffData);
    }

    public PlayerBattleData GetData(int playerId)
    {
        return dataDic[playerId];   //一开始时会先从lua端同步一次，所以这里一定是有数据的
    }

    public BuffData GetBuffData(int playerId)
    {
        return buffDataDic[playerId];   //一开始时会先从lua端同步一次，所以这里一定是有数据的
    }

    private void SyncPlayerData(object sender, GameEventArgs e)
    {
        SyncPlayerDataEventArgs args = e as SyncPlayerDataEventArgs;
        if (!dataDic.ContainsKey(args.Data.id))
        {
            dataDic.Add(args.Data.id, args.Data);
        }

        dataDic[args.Data.id] = args.Data;
    }

    private void BattleStateChange(object sender, GameEventArgs e)
    {
        BattleStateChangeArgs args = e as BattleStateChangeArgs;
        State = args.State;
        Debug.Log("C# 战斗状态改变 状态==" + State);
    }

    private void SyncPlayerBuffData(object sender, GameEventArgs e)
    {
        Debug.Log("同步buff数据到C#端了");
        SyncBuffDataEventArgs args = e as SyncBuffDataEventArgs;
        if(!buffDataDic.ContainsKey(args.playerId))
        {
            buffDataDic.Add(args.playerId, args.data);
        }

        buffDataDic[args.playerId] = args.data;
    }

    /// <summary>
    /// 根据playerId获取玩家实体
    /// </summary>
    /// <param name="playerId"></param>
    /// <returns></returns>
    public PlayerEntity GetPlayerEntity(int playerId)
    {
        IEntityGroup group = Module.Entity.GetEntityGroup("Player");
        IEntity[] entitys = group.GetAllEntities();
        for(int i = 0; i < entitys.Length; i++)
        {
            Entity entity = entitys[i] as Entity;
            PlayerEntity player = entity.Logic as PlayerEntity;
            if(player.PlayerId == playerId)
            {
                return player;
            }
        }

        return null;
    }

    private void OnDisable()
    {
        Module.Event.Unsubscribe(SyncPlayerDataEventArgs.EventId, SyncPlayerData);
        Module.Event.Unsubscribe(BattleStateChangeArgs.EventId, BattleStateChange);
        Module.Event.Unsubscribe(SyncBuffDataEventArgs.EventId, SyncPlayerBuffData);
    }
}
