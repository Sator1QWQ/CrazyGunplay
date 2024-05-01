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
