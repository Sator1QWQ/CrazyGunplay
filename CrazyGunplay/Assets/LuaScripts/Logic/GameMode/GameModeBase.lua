GameModeBase = Class.Create("GameModeBase", Object)

function GameModeBase:ctor()
    self.curControlPlayer = 0
end

--获取当前控制的玩家
function GameModeBase:GetCurControlPlayer()
    return self.curControlPlayer
end

--设置当前控制的是哪个玩家
function GameModeBase:SetCurControlPlayer(playerId)
    self.curControlPlayer = playerId
end