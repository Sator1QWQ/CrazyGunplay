--战斗模式的场景逻辑
--基于LuaBehaviour，由C#调用声明周期
BattleModeScene = Class.Create("BattleModeScene", Object)
function BattleModeScene:ctor()
    self:ChangeBattleState(GlobalEnum.BattleState.None)
end

function BattleModeScene:OnEnable()
    Module.Event:Subscribe(PlayerDieEventArgs.EventId, BattleModeScene.PlayerDieEvent)
    print("注册死亡事件成功")
    self:ChangeBattleState(GlobalEnum.BattleState.Battle)
    Module.Timer:AddUpdateTimer(function(data)
        --print("countDown==" .. tostring(data.remainTime))
        local isEnd, allDeadTeamId = self:CheckBattleEnd()
        if isEnd then
            print("队伍全灭 游戏结束 teamId==" .. tostring(allDeadTeamId))
            self:ChangeBattleState(GlobalEnum.BattleState.End)
        end
    end,
    function()
        print("倒计时结束，游戏结束，返回选择菜单")
        self:ChangeBattleState(GlobalEnum.BattleState.End)
        --UI:OpenUIForm("Assets/Resource/UI/BattleEndPanel.prefab", "NormalGroup")
    end, 0, MBattleSetting.Instance.countDown)
end

--清除战斗数据
function BattleModeScene:ClearBattleData()
    MPlayer.Instance:Clear()
    MTeam.Instance:Clear()
    Module.Timer:RemoveAllTimer()
    Module.Event:Unsubscribe(PlayerDieEventArgs.EventId, PlayerDieEvent)
end

--战斗结束判断 需要提取出来
function BattleModeScene:CheckBattleEnd()
    --其中一方所有生命值归零，则结束
    local teamTable = MTeam.Instance.teamTable
    local allDeadTeamId
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
    return isBattleEnd, allDeadTeamId
end

function BattleModeScene.PlayerDieEvent(sender, args)
    print("lua 玩家死亡！self==" .. tostring(sender) .. ", id==" .. tostring(args.PlayerId) .. ", args==" .. tostring(args.NowLife))
    MPlayer.Instance:ChangeLife(args.PlayerId, -1)
end

function BattleModeScene:ChangeBattleState(state)
    self.battleState = state
    local args = CS.BattleStateChangeArgs.Create(state)
    Module.Event:FireNow(nil, args)
end

BattleModeScene.Instance = BattleModeScene.new()
return BattleModeScene.Instance --使用LuaBehaviour的都要return实例