--战斗模式的场景逻辑
--基于LuaBehaviour，由C#调用声明周期
BattleModeScene = Class.Create("BattleModeScene", Object)

function BattleModeScene:OnEnable()
    print("Hello!!!")
    Module.Timer:AddUpdateTimer(function(data)
        print("countDown==" .. tostring(data.remainTime))
    end,
    function()
        print("倒计时结束！")
    end, 0, MBattleSetting.Instance.countDown)
end

BattleModeScene.Instance = BattleModeScene.new()
return BattleModeScene.Instance