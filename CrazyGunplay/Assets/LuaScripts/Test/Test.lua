Test = Class.Create("Test", Object)

function Test:Awake()
    self.oneP = { id = GlobalDefine.OnePId, name = GlobalDefine.OnePName, heroId = 1001, life = MBattleSetting.Instance.playerLife, weaponId = 101 }
    self.twoP = { id = GlobalDefine.TwoPId, name = GlobalDefine.TwoPName, heroId = 1001, life = MBattleSetting.Instance.playerLife, weaponId = 101 }
    MPlayer.Instance:AddPlayer(self.oneP)
    --MPlayer.Instance:AddPlayer(self.twoP)
    MTeam.Instance:AddTeamPlayer(GlobalDefine.BlueTeam, GlobalDefine.OnePId)
    --MTeam.Instance:AddTeamPlayer(GlobalDefine.RedTeam, GlobalDefine.OnePId)
    CPlayer.Instance:SyncBattleDataToCS(self.oneP.id)
    print("刚同步数据")
    --CPlayer.Instance:SyncBattleDataToCS(self.twoP.id)
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