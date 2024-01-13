--游戏玩法模式
GamePlayModeBase = Class.Create("GamePlayModeBase", Object)

local _P = {}

function GamePlayModeBase:StartCountDown()
    self.countDown = MBattleSetting.Instance.countDown
    Timer:AddUpdateTimer(function(timer) 
        _P.Update(self, timer)
    end, nil, 0, -1)
end

function _P.Update(self, timer)
    Debug.Log("countdown == ".. tostring(self.countDown))
    if self.countDown <= 0 then
        Timer:RemoveTimer(timer)
    end 
    self.countDown = self.countDown - Time.deltaTime
end