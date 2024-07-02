require "Configs.Config.Weapon"
require "Configs.Config.Buff"

--战斗模式的场景逻辑
--基于LuaBehaviour，由C#调用声明周期
BattleModeScene = Class.Create("BattleModeScene", Object)

function BattleModeScene:Awake()
    self:ChangeBattleState(GlobalEnum.BattleState.None)
    self.winnerTeam = nil
end

function BattleModeScene:OnEnable()
    Module.Event:Subscribe(PlayerDieEventArgs.EventId, BattleModeScene.PlayerDieEvent)
    Module.Event:Subscribe(CS.HitEventArgs.EventId, BattleModeScene.HitPlayerEvent)
    Module.Event:Subscribe(CS.BuffStartEventArgs.EventId, BattleModeScene.BuffStartEvent)
    Module.Event:Subscribe(CS.BuffEndEventArgs.EventId, BattleModeScene.BuffEndEvent)
    
    ModeFactory.Instance.currentMode:CreatePlayers()
    self:ChangeBattleState(GlobalEnum.BattleState.Battle)    
    Module.Timer:AddUpdateTimer(function(data)
        local isEnd, allDeadTeamId, winnerTeam = ModeFactory.Instance.currentMode:CheckBattleEnd()
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
        UITool.OpenUIForm("BattleEndPanel", false)
        --UI:OpenUIForm("Assets/Resource/UI/BattleEndPanel.prefab", "NormalGroup")
    end, 0, MBattleSetting.Instance.countDown)

    --显示HUD
    UITool.OpenUIForm("LocalBattleMainPanel", true)
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
    Module.Event:Unsubscribe(PlayerDieEventArgs.EventId, BattleModeScene.PlayerDieEvent)
    Module.Event:Unsubscribe(CS.HitEventArgs.EventId, BattleModeScene.HitPlayerEvent)
    Module.Event:Unsubscribe(CS.BuffStartEventArgs.EventId, BattleModeScene.BuffStartEvent)
    Module.Event:Unsubscribe(CS.BuffEndEventArgs.EventId, BattleModeScene.BuffEndEvent)
end

function BattleModeScene.PlayerDieEvent(sender, args)
    print("lua 玩家死亡！self==" .. tostring(sender) .. ", id==" .. tostring(args.PlayerId) .. ", args==" .. tostring(args.NowLife))
    MPlayer.Instance:ChangeLife(args.PlayerId, -1)
end

--击中事件
function BattleModeScene.HitPlayerEvent(sender, args)
    local beatBackValue = 0
    local data = args.Data
    
    --此处的buffList为C#传来的List<int>对象，索引为0开始，所以不能用ipairs遍历
    for i, buffValue in pairs(data.buffList) do
        -- --该buff为直接扣血的话，则设定击退值
        if buffValue.buffId == GlobalDefine.GetHurtBuff then
            beatBackValue = beatBackValue + buffValue.value
        else
            CBuff.Instance:AddBuff(data.receiverId, buffValue.buffId, buffValue.duration, buffValue.value)
        end
    end

    if data.hitType ~= GlobalEnum.HitType.No then
        MPlayer.Instance:ChangeBeatBackValue(data.receiverId, beatBackValue)    
    end

    local args = CS.PlayerGetDamageEventArgs.Create(data.receiverId, data.hitType, data.direction)
    Module.Event:FireNow(nil, args)
end

function BattleModeScene:ChangeBattleState(state)
    self.battleState = state
    local args = CS.BattleStateChangeArgs.Create(state)
    Module.Event:FireNow(nil, args)
end

function BattleModeScene.BuffStartEvent(sender, args)
    MPlayer.Instance:BuffStart(args.playerId, args.key, args.value)
end

function BattleModeScene.BuffEndEvent(sender, args)
    MPlayer.Instance:BuffEnd(args.playerId, args.key)
end

BattleModeScene.Instance = BattleModeScene.new()
return BattleModeScene.Instance --使用LuaBehaviour的都要return实例