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

BattleModeScene.Instance = BattleModeScene.new()
return BattleModeScene.Instance --使用LuaBehaviour的都要return实例