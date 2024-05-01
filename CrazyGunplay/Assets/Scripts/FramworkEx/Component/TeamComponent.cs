using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using XLua;

[CSharpCallLua]
public interface TeamMemberData
{
    int teamId { get; set; }
    int playerId { get; set; }
}

/// <summary>
/// 队伍数据 由lua同步
/// </summary>
public class TeamComponent : GameFrameworkComponent
{
    private Dictionary<int, List<TeamMemberData>> teamDic = new Dictionary<int, List<TeamMemberData>>();

    private void Start()
    {
        Module.Event.Subscribe(SyncTeamDataEventArgs.EventId, SyncTeamData);
    }

    /// <summary>
    /// 两个玩家是否同一个队伍
    /// </summary>
    /// <param name="playerId"></param>
    /// <param name="otherPlayer"></param>
    /// <returns></returns>
    public bool IsSameTeam(int playerId, int otherPlayer)
    {
        int team1 = GetTeamId(playerId);
        int team2 = GetTeamId(otherPlayer);
        if (team1 != -1 && team2 != -1)
        {
            return team1 == team2;
        }

        return false;
    }

    /// <summary>
    /// 获取玩家的队伍id
    /// </summary>
    /// <param name="playerId"></param>
    /// <returns>没在字典里则返回-1</returns>
    public int GetTeamId(int playerId)
    {
        foreach (KeyValuePair<int, List<TeamMemberData>> pairs in teamDic)
        {
            List<TeamMemberData> members = pairs.Value;
            foreach(TeamMemberData item in members)
            {
                if(item.playerId == playerId)
                {
                    return pairs.Key;
                }
            }
        }

        return -1;
    }

    private void SyncTeamData(object sender, GameEventArgs e)
    {
        SyncTeamDataEventArgs args = e as SyncTeamDataEventArgs;
        List<TeamMemberData> teamMemberList = args.Data;
        
        if(!teamDic.ContainsKey(teamMemberList[0].teamId))
        {
            teamDic.Add(teamMemberList[0].teamId, teamMemberList);
        }
        else
        {
            teamDic[teamMemberList[0].teamId] = teamMemberList;
        }
    }

    private void OnDisable()
    {
        Module.Event.Unsubscribe(SyncTeamDataEventArgs.EventId, SyncTeamData);
    }
}
