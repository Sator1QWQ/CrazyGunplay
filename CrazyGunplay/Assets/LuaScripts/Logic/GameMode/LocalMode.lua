--本地模式,1v1
LocalMode = Class.Create("LocalMode", GameModeBase)
function LocalMode:ctor()
    self.oneP = {id = GlobalDefine.OnePId, name = GlobalDefine.OnePName}
    self.twoP = {id = GlobalDefine.TwoPId, name = GlobalDefine.TwoPName}
    MPlayer.Instance:AddPlayer(self.oneP)
    MPlayer.Instance:AddPlayer(self.twoP)
end