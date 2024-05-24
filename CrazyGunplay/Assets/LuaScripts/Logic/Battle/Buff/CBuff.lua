require "Configs.Config.Buff"

CBuff = Class.Create("CBuff", Object)
local _P = {}

function CBuff:InitData(playerId)
    local player = MPlayer.Instance.playerList[playerId]
    player.buffDic = {}

    local defaultBuffData = MPlayer.Instance.buffDefaultData
    player.buffData = {
        moveSpeedScale = defaultBuffData.moveSpeedScale,
        getBeatScale = defaultBuffData.getBeatScale,
        hpAdd = defaultBuffData.hpAdd,
        fireRateScale = defaultBuffData.fireRateScale,
        attackScale = defaultBuffData.attackScale,
    }
    self:SyncBuffData(playerId)
end

function CBuff:AddBuff(playerId, buffId, duration, buffValue)
    print("CBuff 添加buff")
    local player = MPlayer.Instance.playerList[playerId]

    if player.buffDic[buffId] ~= nil then
        --原先的定时器直接终止，重新开始计时
        Timer:RemoveTimersByTag("Buff_" .. tostring(buffId))
    end

    player.buffDic[buffId] = {
        id = buffId,
        duration = duration,
        continueTime = 0,
        remainTime = 0,
        value = 0,  --buff数值
    }
    player.buffDic[buffId].value = buffValue

    local isTriggerOnce = Buff[buffId].triggerInterval == -1
    local trigger = false
    local tempTime = 0
    Timer:AddUpdateTimer(
    --每帧回调
    function(data)
        print("CBuff buff每帧回调")
        player.buffDic[buffId].continueTime = data.continueTime
        player.buffDic[buffId].remainTime = data.remainTime

        if not isTriggerOnce then
            tempTime = tempTime + Time.deltaTime
            if tempTime >= Buff[buffId].triggerInterval then
                _P.CaculateBuffData(playerId)
                self:SyncBuffData(playerId)
                tempTime = 0
            end
        elseif isTriggerOnce and not trigger then
            _P.CaculateBuffData(playerId)
            self:SyncBuffData(playerId)
            trigger = true
        end
    end, 

    --结束回调 移除buff
    function(data)
        player.buffDic[buffId] = nil
        _P.CaculateBuffData(playerId)
        self:SyncBuffData(playerId)
    end,
    0, duration, nil, "Buff_" .. tostring(buffId))
end

--同步buff数据
function CBuff:SyncBuffData(playerId)
    local player = MPlayer.Instance.playerList[playerId]
    local args = CS.SyncBuffDataEventArgs.Create(playerId, player.buffData, player.buffDic)
    Module.Event:FireNow(nil, args)
end

--计算出某一时刻的BuffData数据
function _P.CaculateBuffData(playerId)
    local player = MPlayer.Instance.playerList[playerId]
    local defaultData = MPlayer.Instance.buffDefaultData
    local valueTable = {
        moveSpeedScale = defaultData.moveSpeedScale,
        getBeatScale = defaultData.getBeatScale,
        hpAdd = defaultData.hpAdd,
        fireRateScale = defaultData.fireRateScale,
        attackScale = defaultData.attackScale,
    }
    for k, buffData in pairs(player.buffDic) do
        local buffType = Buff[buffData.id].type
        local operatorType = Buff[buffData.id].operatorType
        local triggerInterval = Buff[buffData.id].triggerInterval
        local valueKey = GlobalEnum.BuffKey[buffType]
        local resultValue = nil
        
        print("CBuff Buffid==" .. tostring(buffData.id) .. ", valueKey==" .. tostring(valueKey) .. ", value==" .. tostring(value))
        if triggerInterval ~= -1 then
            --累加的buff效果
            resultValue = _P.Operator(operatorType, defaultData[valueKey], (buffData.continueTime / triggerInterval) * buffData.value)
        else
            resultValue = _P.Operator(operatorType, defaultData[valueKey], buffData.value)
        end

        valueTable[valueKey] = resultValue
    end

    --直接覆盖掉原来的数据
    player.buffData = valueTable
    return valueTable
end

--根据操作符计算结果
function _P.Operator(operatorType, defaultValue, value)
    local opType = GlobalEnum.OperatorType
    local result = nil
    if operatorType == opType.Add then
        result = defaultValue + value
    elseif operatorType == opType.Sub then
        result = defaultValue - value
    elseif operatorType == opType.Multi then
        result = defaultValue * value
    elseif operatorType == opType.Divi then
        result = defaultValue / value
    end

    return result
end

CBuff.Instance = CBuff.new()