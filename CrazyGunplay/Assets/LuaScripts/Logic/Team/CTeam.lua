CTeam = Class.Create("CTeam", CTeam)

function CTeam:ctor()
end

--同步1个队伍
function CTeam:SyncTeamDataToCS(teamId)
    local team = MTeam.Instance.teamTable[teamId]
    local syncData = {}
    for _, v in pairs(team) do
        local data = {
            teamId = teamId,
            playerId = v.playerId
        }
        table.insert(syncData, data)
    end
    local args = CS.SyncTeamDataEventArgs.Create(syncData)
    Module.Event:FireNow(nil, args)
end

CTeam.Instance = CTeam.new()