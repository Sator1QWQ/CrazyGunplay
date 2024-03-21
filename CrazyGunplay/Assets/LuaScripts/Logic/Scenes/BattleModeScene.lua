--战斗模式的场景逻辑
--基于LuaBehaviour，由C#调用声明周期
BattleModeScene = Class.Create("BattleModeScene", Object)

function BattleModeScene:OnEnable()
    print("Hello!!!")
    Module.Timer:AddUpdateTimer(function(data)
        print("countDown==" .. tostring(data.remainTime))
        
    end,
    function()
        print("倒计时结束，游戏结束，返回选择菜单")
        UI:OpenUIForm("Assets/Resource/UI/BattleEndPanel.prefab", "NormalGroup")
    end, 0, MBattleSetting.Instance.countDown)
end

--清除战斗数据
function BattleModeScene:ClearBattleData()
    MPlayer.Instance:Clear()
    MTeam.Instance:Clear()
    Module.Timer:RemoveAllTimer()
end

--战斗结束判断 需要提取出来
function BattleModeScene:CheckBattleEnd()
    --其中一方所有生命值归零，则结束
    local teamTable = MTeam.Instance.teamTable
    local allDeadTeamId = true
    for teamId, pList in pairs(teamTable) do
        local isPlayerDead = true
        for i, playerId in ipairs(pList) do
            local playerData = MPlayer.Instance.playerList[playerId]
            if playerData.life > 0 then
                isPlayerDead = false
            end
        end

        if isPlayerDead then
            allDeadTeamId = teamId
            break   --直接停止循环，不管剩下的列表
        end
    end

    local isBattleEnd = allDeadTeamId ~= nil
    if isBattleEnd then
        print("队伍全灭 游戏结束 teamId==" .. tostring(allDeadTeamId))    
    end
    
    return isBattleEnd
end

BattleModeScene.Instance = BattleModeScene.new()
return BattleModeScene.Instance --使用LuaBehaviour的都要return实例