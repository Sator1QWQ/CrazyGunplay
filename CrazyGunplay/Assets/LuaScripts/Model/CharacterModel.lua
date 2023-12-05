--选择角色时的model
CharacterModel = {}
local _P = {}

--设置游戏模式
function CharacterModel.SetMode(mode)
    _P.mode = mode
end

function CharacterModel.GetMode()
    return _P.mode
end

--当前选中的英雄
function CharacterModel.SetSelectPlayer(playerId, id)
    _P.selectCharacterId[playerId] = id
end

function CharacterModel.GetSelectPlayer(playerId)
    return _P.selectCharacterId[playerId]
end