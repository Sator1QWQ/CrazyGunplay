using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTeamDataEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(SyncTeamDataEventArgs).GetHashCode();

    public override int Id => EventId;

    public List<TeamMemberData> Data { get; private set; }

    public static SyncTeamDataEventArgs Create(List<TeamMemberData> data)
    {
        SyncTeamDataEventArgs e = ReferencePool.Acquire<SyncTeamDataEventArgs>();
        e.Data = data;
        return e;
    }

    public override void Clear()
    {
        Data = null;
    }
}
