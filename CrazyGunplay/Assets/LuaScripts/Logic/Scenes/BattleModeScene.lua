--战斗模式的场景逻辑
--基于LuaBehaviour，由C#调用声明周期
BattleModeScene = Class.Create("BattleModeScene", Object)

function BattleModeScene:Awake()
    self:ChangeBattleState(GlobalEnum.BattleState.None)
    self.winnerTeam = nil
end

function BattleModeScene:OnEnable()
    Module.Event:Subscribe(PlayerDieEventArgs.EventId, BattleModeScene.PlayerDieEvent)
    self:ChangeBattleState(GlobalEnum.BattleState.Battle)
    Module.Timer:AddUpdateTimer(function(data)
        --print("countDown==" .. tostring(data.remainTime))
        local isEnd, allDeadTeamId, winnerTeam = self:CheckBattleEnd()
        if isEnd then
            self.winnerTeam = winnerTeam
            print("队伍全灭 游戏结束 teamId==" .. tostring(allDeadTeamId))
            self:ChangeBattleState(GlobalEnum.BattleState.TeamDead)
            UITool.OpenUIForm("BattleEndPanel", true)
            Module.Timer:RemoveTimer(data)
        end
    end,
    function()
        print("倒计时结束，游戏结束，返回选择菜单")
        self:ChangeBattleState(GlobalEnum.BattleState.Timeout)
        UITool.OpenUIForm("BattleEndPanel", true)
        --UI:OpenUIForm("Assets/Resource/UI/BattleEndPanel.prefab", "NormalGroup")
    end, 0, MBattleSetting.Instance.countDown)
end

function BattleModeScene:OnDisable()
    self:ChangeBattleState(GlobalEnum.BattleState.None)
    self.winnerTeam = nil
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
    local winnerTeam
    for teamId, pList in pairs(teamTable) do
        local isPlayerDead = true
        for i, playerId in ipairs(pList) do
            local playerData = MPlayer.Instance.playerList[playerId]
            if playerData.life > 0 then
                isPlayerDead = false
            end
        end

        if isPlayerDead then
            print("team1 deadTeamId==" .. tostring(teamId))
            allDeadTeamId = teamId
            break   --直接停止循环，不管剩下的列表
        end
    end

    --winnerTeam，除了allDeadTeam以外的第一个队伍获胜
    for teamId in pairs(teamTable) do
        print("team1 teamId==" .. tostring(teamId))
        if allDeadTeamId ~= nil and allDeadTeamId ~= teamId then
            winnerTeam = teamId
        end
    end

    local isBattleEnd = allDeadTeamId ~= nil
    return isBattleEnd, allDeadTeamId, winnerTeam
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