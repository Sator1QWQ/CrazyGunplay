require "Configs.Config.Character"
local _M = {}

function _M.OnInit(panel)
    local grid = panel:Get("CharacterGrid_grid")
    grid:SetCount(Character.Count)
    panel:Get("OK_btn").onClick:AddListener(function()
        _M.ClickOK(grid)
    end)
    panel:Get("OK_text").text = Text.OK
    panel:Get("Back_text").text = Text.Back
end

function _M.ClickOK(grid)
    local curSelect = grid:GetCurSelect()
    Debug.Log("curSelect==" .. tostring(curSelect))
    if curSelect == -1 then
        return
    end

    local heroId = 1000 + curSelect + 1
    local modeObj = ModeFactory.Instance:GetModeObj()
    local playerId = modeObj:GetCurControlPlayer()
    MHero.Instance:SetSelectPlayer(playerId, heroId)
    Debug.Log("当前玩家==" .. tostring(playerId) .. ", 选中了==" .. tostring(heroId))
end

function _M.ClickBack()
    MHero.Instance:ClearSelectHero()
end

return _M