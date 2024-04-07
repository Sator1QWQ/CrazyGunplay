CPlayer = Class.Create("CPlayer", CPlayer)

function CPlayer:ctor()
end

--玩家数据同步到CS端
function CPlayer:SyncBattleDataToCS(playerId)
    local player = MPlayer.Instance.playerList[playerId]
    local data = CS.PlayerBattleData()
    data.PlayerId = player.id
    data.HeroId = player.heroId
    data.WeaponId = player.weaponId
    data.Life = player.life
    data.BeatBackValue = player.beatBackValue
    data.BeatBackPercent = player.beatBackPercent
    local args = CS.SyncPlayerDataEventArgs.Create(data)
    Module.Event:FireNow(nil, args)
end

CPlayer.Instance = CPlayer.new()