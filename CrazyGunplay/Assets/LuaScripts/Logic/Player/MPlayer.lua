--玩家数据  给战斗用的
MPlayer = Class.Create("MPlayer", Object)

function MPlayer:ctor()
    self.playerList = {}
    self.count = 0
    self.buffDefaultData = {
        moveSpeedScale = 1,
        getBeatScale = 1,   --受伤的倍率
        hpAdd = 0,
        fireRateScale = 1,
        attackScale = 1,  --增加伤害的倍率
    }
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
    self.count = self.count + 1
    data.index = self.count  --在列表里的索引，要保证遍历的顺序
    data.beatBackValue = 0
    data.beatBackPercent = 0

    --由buff系统操作的数据，在C#中也需要存
    data.buffData = {
        moveSpeedScale = self.buffDefaultData.moveSpeedScale,
        getBeatScale = self.buffDefaultData.getBeatScale,
        hpAdd = self.buffDefaultData.hpAdd,
        fireRateScale = self.buffDefaultData.fireRateScale,
        attackScale = self.buffDefaultData.attackScale,
    }

    CPlayer.Instance:SyncBattleDataToCS(data.id)
    CBuff.Instance:InitData(data.id)
end

function MPlayer:Clear()
    self.playerList = {}
    self.count = 0
end

function MPlayer:ChangeLife(id, change)
    self.playerList[id].life = self.playerList[id].life + change
    CPlayer.Instance:SyncBattleDataToCS(id)
end

--击退值改变
function MPlayer:ChangeBeatBackValue(id, value)
    self.playerList[id].beatBackValue = self.playerList[id].beatBackValue + value

    --百分比计算：当击退为0的时候，显示百分比为0
    --击退修正值：保证显示的百分比数不会太大
    if self.playerList[id].beatBackValue == 0 then
        self.playerList[id].beatBackPercent = 0    
    else
        self.playerList[id].beatBackPercent = math.floor((self.playerList[id].beatBackValue * 100) * GlobalDefine.BeatBackRepair)
    end
    CPlayer.Instance:SyncBattleDataToCS(id)
end

function MPlayer:BuffStart(id, key, value)
    self.playerList[id].buffData[key] = value
    CPlayer.Instance:SyncBuffDataToCS(id)
end

function MPlayer:BuffEnd(id, key)
    self.playerList[id].buffData[key] = self.buffDefaultData[key]
    CPlayer.Instance:SyncBuffDataToCS(id)
end

function MPlayer:SetBuffData(id, buffDataTable)
    self.playerList[id].buffData = buffDataTable
end

--根据索引获取数据
function MPlayer:GetPlayerByIndex(index)
    --数据过多的话这种方法有性能问题
    --可以改成字典
    for k, v in pairs(self.playerList) do
        if v.index == index then
            return v
        end
    end
end

--单例模式
MPlayer.Instance = MPlayer.new()