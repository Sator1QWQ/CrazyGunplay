--本地模式,1v1
LocalMode = Class.Create("LocalMode", GameModeBase)

function LocalMode:CreatePlayers()
    local oneP = MPlayer.Instance.playerList[GlobalDefine.OnePId]
    local twoP = MPlayer.Instance.playerList[GlobalDefine.TwoPId]

    local assetOneP = Character[oneP.heroId].model
    local assetTwoP = Character[twoP.heroId].model
    Module.Entity:ShowEntity(123, typeof(CS.PlayerEntity), "Assets/Resource/Models/Player/" .. assetOneP .. ".prefab", "Player", {playerId = oneP.id})
    Module.Entity:ShowEntity(124, typeof(CS.PlayerEntity), "Assets/Resource/Models/Player/" .. assetTwoP .. ".prefab", "Player", {playerId = twoP.id})
end