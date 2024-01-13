--本地模式下的选择界面
require "Configs.Config.Character"
require "Logic.Battle.Modes.NormalGameMode"
require "Logic.Battle.GamePlayModeBase"
local _M = {}

function _M.OnInit(panel)
    local grid = panel:Get("CharacterGrid_grid")
    grid:SetCount(Character.Count)
    panel:Get("OK_btn").onClick:AddListener(function()
        _M.ClickOK(grid)
    end)
    _M.okText = panel:Get("OK_text")
    _M.okText.text = Text.OK
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
    if playerId == 1 then
        _M.isOnePSelect = true
    end
    if playerId == 2 then
        _M.isTwoPSelect = true
    end

    --测试用
    if _M.isOnePSelect and _M.isTwoPSelect then
        local normalMode = NormalGameMode.new()
        MBattleSetting.Instance:SetGameMode(normalMode)
        MBattleSetting.Instance:StartCountDown()
    end

    Debug.Log("当前玩家==" .. tostring(playerId) .. ", 选中了==" .. tostring(heroId))
end

function _M.ClickBack()
    MHero.Instance:ClearSelectHero()
end

function _M.Refresh()
    Debug.Log("Refresh OK== " .. tostring(_M.isOnePSelect))
    if _M.isOnePSelect then
        _M.okText.text = Text.StartBattle
    end
end

return _M