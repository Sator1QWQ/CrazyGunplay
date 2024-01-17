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
    panel:Get("Back_btn").onClick:AddListener(function()
        _M.ClickBack()
    end)
    _M.panel = panel
    _M.grid = grid
end

function _M.OnOpen()
    _M.isOnePSelect = false
    _M.isTwoPSelect = false
    _M.isOnePReady = false
    _M.isTwoPReady = false
    _M.okText.text = Text.OK
    _M.grid:Foreach(function(item)
        item:OnOpen()
    end)
end

function _M.ClickOK(grid)
    local curSelect = grid:GetCurSelect()
    Debug.Log("curSelect==" .. tostring(curSelect))
    if curSelect == -1 then
        return
    end

    local heroId = 1000 + curSelect + 1
    if _M.isOnePSelect and not _M.isOnePReady then
        _M.isOnePReady = true
    end
    if _M.isTwoPSelect and not _M.isTwoPReady then
        _M.isTwoPReady = true
    end

    --测试用
    if _M.isOnePReady and _M.isTwoPReady then
        local normalMode = NormalGameMode.new()
        MBattleSetting.Instance:SetGameMode(normalMode)
        MBattleSetting.Instance:StartCountDown()
    end

    grid:Foreach(function(item)
        item:InitSelect()
    end)
end

function _M.ClickBack()
    _M.panel:Close()
end

function _M.Refresh()
    if _M.isOnePReady then
        _M.okText.text = Text.StartBattle
    end
end

--哪个玩家选了英雄
function _M.PlayerSelectChange(id, b)
    if id == GlobalDefine.OnePId then
        _M.isOnePSelect = b
    else
        _M.isTwoPSelect = b
    end
end

--玩家是否被选中
function _M.GetPlayerSelect(id)
    if id == GlobalDefine.OnePId then
        return _M.isOnePSelect
    else
        return _M.isTwoPSelect
    end
end

return _M