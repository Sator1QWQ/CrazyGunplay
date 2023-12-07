--本地模式,1v1
LocalMode = Class.Create("LocalMode", GameModeBase)
function LocalMode:ctor()
    self.oneP = {id = 1, name = "1P"}
    self.twoP = {id = 2, name = "2P"}
    MPlayer.Instance:AddPlayer(self.oneP)
    MPlayer.Instance:AddPlayer(self.twoP)
end

--当前控制的玩家选择英雄
function LocalMode:GetCurControlPlayer()
    --如果1p已经选择了英雄，则返回2p
    if MHero.Instance.selectHeroDic[self.oneP.id] == nil then
        return self.oneP.id
    else
        return self.twoP.id
    end
end