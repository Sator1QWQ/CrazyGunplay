require "Configs.Config.Character"
Test = Class.Create("Test", Object)

function Test:Awake()
    self.oneP = { id = GlobalDefine.OnePId, name = GlobalDefine.OnePName, heroId = 1001, life = MBattleSetting.Instance.playerLife, weaponId = 101 }
    self.twoP = { id = GlobalDefine.TwoPId, name = GlobalDefine.TwoPName, heroId = 1002, life = MBattleSetting.Instance.playerLife, weaponId = 101 }
    
    Module.Entity:AddEntityGroup("normal", 1, 10, 10, 1)
    --1P
    MPlayer.Instance:AddPlayer(self.oneP)
    MTeam.Instance:AddTeamPlayer(GlobalDefine.BlueTeam, GlobalDefine.OnePId)
    CPlayer.Instance:SyncBattleDataToCS(self.oneP.id)
    local asset1 = Character[self.oneP.heroId].model
    Module.Entity:ShowEntity(123, typeof(CS.PlayerEntity), "Assets/Resource/Models/Player/" .. asset1 .. ".prefab", "normal", {playerId = self.oneP.id})
    --2P
    MPlayer.Instance:AddPlayer(self.twoP)
    MTeam.Instance:AddTeamPlayer(GlobalDefine.RedTeam, GlobalDefine.TwoPId)
    CPlayer.Instance:SyncBattleDataToCS(self.twoP.id)
    local asset2 = Character[self.twoP.heroId].model
    Module.Entity:ShowEntity(124, typeof(CS.PlayerEntity), "Assets/Resource/Models/Player/" .. asset2 .. ".prefab", "normal", {playerId = self.twoP.id})
end

function Test:Update()
    -- if Input.GetKeyDown(KeyCode.K) then
    --     -- local oneP = MPlayer.Instance.playerList[self.oneP.id]
    --     -- oneP.life = oneP.life - 1
    --     -- CPlayer.Instance:SyncBattleDataToCS(oneP.id)    
    -- end
end


Test.Instance = Test.new()
return Test.Instance