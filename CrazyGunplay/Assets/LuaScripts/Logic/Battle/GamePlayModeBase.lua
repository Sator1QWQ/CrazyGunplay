--游戏玩法模式
GamePlayModeBase = Class.Create("GamePlayModeBase", Object)

function GamePlayModeBase:StartCountDown()
    local countDown = MBattleSetting.countDown
    local timer = Timer:AddUpdateTimer(function() 
        Debug.Log("timer==" .. tostring(timer))
        if countDown <= 0 then
            Timer:RemoveTimer(timer)
        end 
    end)
end