MTeam = Class.Create("MTeam", Object)

function MTeam:ctor()
    self.teamTable = {} --队伍表
end

--添加队员
function MTeam:AddTeamPlayer(teamId, playerId)
    if self.teamTable[teamId] == nil then
        self.teamTable[teamId] = {}
    end

    if self:IsPlayerInTeam(teamId, playerId) then
        print("Error！！玩家已经在队伍中 teamId==" .. tostring(teamId) .. ", playerId==" .. tostring(playerId))
    end

    local data = {
        playerId = playerId,
    }
    table.insert(self.teamTable[teamId], data)
    CTeam.Instance:SyncTeamDataToCS(teamId)
end

--移除某个队员
function MTeam:RemoveTeamPlayer(teamId, playerId)
    local playerList = self.teamTable[teamId]
    for i, v in ipairs(playerList) do
        if v.playerId == playerId then
            table.remove(playerList, i)
            break
        end
    end

    CTeam.Instance:SyncTeamDataToCS(teamId)
end

--玩家是否在队中
function MTeam:IsPlayerInTeam(teamId, playerId)
    local playerList = self.teamTable[teamId]
    local isInTeam = false
    for i, v in ipairs(playerList) do
        if v.playerId == playerId then
            isInTeam = true
            break
        end
    end
    return isInTeam
end

function MTeam:Clear()
    self.teamTable = {}
end

MTeam.Instance = MTeam.new()