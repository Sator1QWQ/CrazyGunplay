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

--击飞类型
GlobalEnum.HitType =
{
	BeatBack = 1,	--击退
	HitToFly = 2,	--击飞
	No = 3,	--无效果
}

GlobalEnum.BuffType =
{
	MoveSpeed = 1,  --移速修改
	GetBeatBack = 2,    --受伤时的击退效果修改
	Hp = 3, --血量修改
	FireRate = 4,   --射速修改
	BeatBackValue = 5,  --攻击力修改
}

GlobalEnum.BuffKey = 
{
    [1] = "moveSpeedScale",
    [2] = "getBeatScale",
    [3] = "hpAdd",
    [4] = "fireRateScale",
    [5] = "attackScale",
}

--运算符
GlobalEnum.OperatorType =
{
    Add = 1,    --加
    Sub = 2,    --减
    Multi = 3,  --乘
    Divi = 4,   --除
}
