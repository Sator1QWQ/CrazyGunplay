--本地模式,1v1
LocalMode = Class.Create("LocalMode", GameModeBase)

function LocalMode:CreatePlayers()
    local oneP = MPlayer.Instance.playerList[GlobalDefine.OnePId]
    local twoP = MPlayer.Instance.playerList[GlobalDefine.TwoPId]

    local assetOneP = Character[oneP.heroId].model
    local assetTwoP = Character[twoP.heroId].model

    local dataOne = CS.PreloadData()
    local dataTwo = CS.PreloadData()
    dataOne.path = "Assets/Resource/Models/Player/" .. assetOneP .. ".prefab"
    dataTwo.path = "Assets/Resource/Models/Player/" .. assetTwoP .. ".prefab"
    dataOne.type = GlobalEnum.PreloadType.PlayerEntity
    dataTwo.type = GlobalEnum.PreloadType.PlayerEntity
    dataOne.values = {123, typeof(CS.PlayerEntity), "Player", {playerId = oneP.id}}
    dataTwo.values = {124, typeof(CS.PlayerEntity), "Player", {playerId = twoP.id}}

    Module.Preload:AddPreload(dataOne)
    Module.Preload:AddPreload(dataTwo)
    --Module.Entity:ShowEntity(123, typeof(CS.PlayerEntity), "Assets/Resource/Models/Player/" .. assetOneP .. ".prefab", "Player", {playerId = oneP.id})
    --Module.Entity:ShowEntity(124, typeof(CS.PlayerEntity), "Assets/Resource/Models/Player/" .. assetTwoP .. ".prefab", "Player", {playerId = twoP.id})
end