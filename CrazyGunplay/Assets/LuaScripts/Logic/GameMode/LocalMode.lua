--本地模式,1v1
LocalMode = Class.Create("LocalMode", Object)
function LocalMode:ctor()
    self.oneP = {id = 1, name = "1P"}
    self.twoP = {id = 2, name = "2P"}
    PlayerModel.Instance:SetPlayer(self.oneP)
    PlayerModel.Instance:SetPlayer(self.twoP)
end