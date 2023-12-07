--玩家数据
MPlayer = Class.Create("MPlayer", Object)

function MPlayer:ctor()
    self.playerList = {}
    self.playerHeroDic = {}    --玩家当前使用的英雄
end

--[[
    data = {
        id,
        name,   --玩家名字
    }
--]]
function MPlayer:AddPlayer(data)
    if self.playerList[data.id] ~= nil then
        Debug.LogError("玩家被设置过了！ id==" .. tostring(data.id))
        return
    end
    self.playerList[data.id] = data
end

--设置玩家使用的英雄
function MPlayer:SetPlayerHeroData(data)
    --data.id -> playerId
    if self.playerHeroDic[data.id] == nil then
        self.playerHeroDic[data.id] = {}
    end
    self.playerHeroDic[data.id] = data  --允许中途切换英雄
end

--单例模式
MPlayer.Instance = MPlayer.new()