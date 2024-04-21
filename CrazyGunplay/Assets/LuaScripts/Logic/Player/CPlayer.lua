CPlayer = Class.Create("CPlayer", CPlayer)

function CPlayer:ctor()
end

--玩家数据同步到CS端
--不包括buff数据，虽然这里也会传，但是C#端不会获取buff
function CPlayer:SyncBattleDataToCS(playerId)
    local player = MPlayer.Instance.playerList[playerId]
    local args = CS.SyncPlayerDataEventArgs.Create(player)
    Module.Event:FireNow(nil, args)
end

--玩家buff数据同步到CS端
function CPlayer:SyncBuffDataToCS(playerId)
    local buffData = MPlayer.Instance.playerList[playerId].buffData
    local args = CS.SyncBuffDataEventArgs.Create(playerId, buffData)
    Module.Event:FireNow(nil, args)
end

CPlayer.Instance = CPlayer.new()