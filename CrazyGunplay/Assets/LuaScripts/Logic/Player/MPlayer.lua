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

function MPlayer:Clear()
    self.playerList = {}
end

function MPlayer:ChangeLife(id, change)
    self.playerList[id].life = self.playerList[id].life + change
    print("change life 同步数据")
    CPlayer.Instance:SyncBattleDataToCS(id)
end

--单例模式
MPlayer.Instance = MPlayer.new()