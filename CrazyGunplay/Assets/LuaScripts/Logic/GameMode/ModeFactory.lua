--游戏模式工厂
ModeFactory = Class.Create("ModeFactory", Object)
local GameMode = GlobalEnum.GameMode

function ModeFactory:ctor()
    self.modeDic = {}   --mode对象字典
    self.currentMode = nil
end

function ModeFactory:Init()
    self.modeDic[GameMode.Local] = LocalMode.new()
    --self.modeDic[GameMode.Multi] = Multi.new()
    --self.modeDic[GameMode.Stroy] = Story.new()
end

--改变模式
function ModeFactory:ChangeMode(gameMode)
    if self.currentMode == nil then
        self.currentMode = self.modeDic[gameMode]
        return
    end

    self.currentMode:Clear()
    self.currentMode = self.modeDic[gameMode]
end

function ModeFactory:Clear()
    self.modeDic = nil
end

ModeFactory.Instance = ModeFactory.new()