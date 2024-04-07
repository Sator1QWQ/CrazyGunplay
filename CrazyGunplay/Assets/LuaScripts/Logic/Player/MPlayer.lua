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
    data.beatBackValue = 1
    data.beatBackPercent = 0
end

function MPlayer:Clear()
    self.playerList = {}
end

function MPlayer:ChangeLife(id, change)
    self.playerList[id].life = self.playerList[id].life + change
    CPlayer.Instance:SyncBattleDataToCS(id)
end

--击退值改变
function MPlayer:ChangeBeatBackValue(id, value)
    print("id==" .. tostring(id) .. ", value==" .. tostring(value))
    self.playerList[id].beatBackValue = self.playerList[id].beatBackValue + value

    --百分比计算：-1是当击退为1的时候，显示百分比为0
    --击退修正值：保证显示的百分比数不会太大
    self.playerList[id].beatBackPercent = math.floor(((self.playerList[id].beatBackValue - 1) * 100) * GlobalDefine.BeatBackRepair)
    CPlayer.Instance:SyncBattleDataToCS(id)
end

--单例模式
MPlayer.Instance = MPlayer.new()