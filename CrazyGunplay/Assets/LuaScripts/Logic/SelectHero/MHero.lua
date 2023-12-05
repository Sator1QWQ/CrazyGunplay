--选择角色时的model
MHero = {}
local _P = {}

--设置游戏模式
function MHero.SetMode(mode)
    _P.mode = mode
end

function MHero.GetMode()
    return _P.mode
end

--当前选中的英雄
function MHero.SetSelectPlayer(playerId, id)
    _P.selectHeroId[playerId] = id
end

function MHero.GetSelectPlayer(playerId)
    return _P.selectHeroId[playerId]
end