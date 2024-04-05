GlobalEnum = {}

--游戏类型
GlobalEnum.GameMode = {
    Local = 1,  --本地游戏
    Multi = 2,  --多人游戏
    Stroy = 3,  --故事游戏
}

GlobalEnum.SceneType = {
    Start = 1,  --只有开始场景的类型是1
    Battle = 2, --战斗场景
}

--战斗状态
GlobalEnum.BattleState = {
    None = 1,
    Ready = 2,  --准备状态
    Battle = 3, --战斗状态
    TeamDead = 4,    --队伍全灭
    Timeout = 5,    --战斗倒计时结束
}