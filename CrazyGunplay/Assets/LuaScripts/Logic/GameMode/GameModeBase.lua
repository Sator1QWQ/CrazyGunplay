GameModeBase = Class.Create("GameModeBase", Object)

--生成玩家实体
function GameModeBase:CreatePlayers()
    Debug.LogError("必须实现函数！")
end

function GameModeBase:Update()
end

--对局结束判断，为true需要返回3个值：战斗结果，阵亡队伍，获胜队伍
function GameModeBase:CheckBattleEnd()
    --其中一方所有生命值归零，则结束
    local teamTable = MTeam.Instance.teamTable
    local allDeadTeamId
    local winnerTeam
    for teamId, pList in pairs(teamTable) do
        local isPlayerDead = true
        for i, teamData in ipairs(pList) do
            local playerId = teamData.playerId
            local playerData = MPlayer.Instance.playerList[playerId]
            if playerData.life > 0 then
                isPlayerDead = false
            end
        end

        if isPlayerDead then
            allDeadTeamId = teamId
            break   --直接停止循环，不需要管剩下的列表
        end
    end

    --winnerTeam，除了allDeadTeam以外的第一个队伍获胜
    for teamId in pairs(teamTable) do
        if allDeadTeamId ~= nil and allDeadTeamId ~= teamId then
            winnerTeam = teamId
        end
    end

    local isBattleEnd = allDeadTeamId ~= nil
    return isBattleEnd, allDeadTeamId, winnerTeam
end

function GameModeBase:Clear()
end

GameModeBase.Instance = GameModeBase.new()