--游戏模式工厂
require "Logic.GameMode.LocalMode"
ModeFactory = Class.Create("ModeFactory", Object)

--根据游戏模式枚举获取mode对象
function ModeFactory:GetModeObj()
    local mode = MHero.Instance.mode
    local GameMode = GlobalEnum.GameMode
    if self.modeDic[mode] ~= nil then
        return self.modeDic[mode]
    end

    local modeObj = nil
    if mode == GameMode.Local then
        modeObj = LocalMode.new()
    elseif mode == GameMode.Multi then

    else

    end

    if modeObj then
        self.modeDic[mode] = modeObj
    end

    return modeObj
end

function ModeFactory:ctor()
    self.modeDic = {}   --mode对象字典
end

function ModeFactory:Dispose()
    self.modeDic = nil
end

--单例类
ModeFactory.Instance = ModeFactory.new()