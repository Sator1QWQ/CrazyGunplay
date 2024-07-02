GameModeBase = Class.Create("GameModeBase", Object)

--生成玩家实体
function GameModeBase:CreatePlayers()
    Debug.LogError("必须实现函数！")
end

function GameModeBase:Update()
end

--对局结束条件
function GameModeBase:EndCheck()
    return false
end

function GameModeBase:Clear()
end

GameModeBase.Instance = GameModeBase.new()