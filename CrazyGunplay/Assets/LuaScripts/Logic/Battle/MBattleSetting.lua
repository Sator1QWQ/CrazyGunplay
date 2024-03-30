MBattleSetting = Class.Create("MBattleSetting", Object)

function MBattleSetting:ctor()
    self.countDown = 9999  --倒计时
    self.playerLife = 3 --玩家有几条命
    self.HPScale = 1  --血量系数，越高越难积累击飞值
end

--设置游戏模式对象
function MBattleSetting:SetGameMode(mode)
    self.gameMode = mode
end

function MBattleSetting:StartCountDown()
    self.gameMode:StartCountDown()
end

MBattleSetting.Instance = MBattleSetting.new()