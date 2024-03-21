--玩家数据  给战斗用的
MPlayer = Class.Create("MPlayer", Object)

function MPlayer:ctor()
    self.playerList = {}
end

--[[
    data = {
        id,
        name,   --玩家名字
        ...
    }
--]]
function MPlayer:AddPlayer(data)
    if self.playerList[data.id] ~= nil then
        Debug.LogError("玩家被设置过了！ id==" .. tostring(data.id))
        return
    end
    self.playerList[data.id] = data
end

--数据同步到CS端
function MPlayer:SyncDataToCS(playerId)
    --Module.Event:
end

function MPlayer:Clear()
    self.playerList = {}
end

--单例模式
MPlayer.Instance = MPlayer.new()