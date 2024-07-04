--本地模式下的选择界面
require "Configs.Config.Character"
require "Logic.Battle.Modes.NormalGameMode"
require "Logic.Battle.GamePlayModeBase"
require "Configs.Config.Scene"
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
    panel:Get("Left_btn").onClick:AddListener(_M.LeftBtnClick)
    panel:Get("Right_btn").onClick:AddListener(_M.RightBtnClick)
    _M.panel = panel
    _M.grid = grid
    _M.sceneNameText = panel:Get("SceneName_text")

    --初始化场景列表
    _M.sceneList = {}
    Debug.Log("Scene==" .. tostring(Scene))
    for id, config in pairs(Scene) do
        if id ~= "Count" then
            if config.sceneType == GlobalEnum.SceneType.Battle then
                table.insert(_M.sceneList, id)
            end    
        end
    end
    table.sort(_M.sceneList, function(a, b)
        return a < b
    end)
end

function _M.OnOpen()
    _M.isOnePSelect = false
    _M.isTwoPSelect = false
    _M.isOnePReady = false
    _M.isTwoPReady = false
    _M.onePSelectId = nil
    _M.twoPSelectId = nil
    _M.okText.text = Text.OK
    _M.sceneIndex = nil
    _M.grid:Foreach(function(item)
        item:OnOpen()
    end)
    _M.RefreshScene()
end

function _M.ClickOK(grid)
    local curSelect = grid:GetCurSelect()
    if curSelect == -1 then
        return
    end

    if _M.isOnePSelect and not _M.isOnePReady then
        _M.isOnePReady = true
    end
    if _M.isTwoPSelect and not _M.isTwoPReady then
        _M.isTwoPReady = true
    end

    if _M.isOnePReady and _M.isTwoPReady then
        _M.ReadyToChangeScene()
        --local normalMode = NormalGameMode.new()
        --MBattleSetting.Instance:SetGameMode(normalMode)
        --MBattleSetting.Instance:StartCountDown()
    end

    grid:Foreach(function(item)
        item:InitSelect()
    end)
end

function _M.ReadyToChangeScene()
    local oneP = {id = GlobalDefine.OnePId, name = GlobalDefine.OnePName, heroId = _M.onePSelectId, life = MBattleSetting.Instance.playerLife}
    local twoP = {id = GlobalDefine.TwoPId, name = GlobalDefine.TwoPName, heroId = _M.twoPSelectId, life = MBattleSetting.Instance.playerLife}
    MPlayer.Instance:AddPlayer(oneP)
    MPlayer.Instance:AddPlayer(twoP)
    MTeam.Instance:AddTeamPlayer(GlobalDefine.BlueTeam, GlobalDefine.OnePId)
    MTeam.Instance:AddTeamPlayer(GlobalDefine.RedTeam, GlobalDefine.TwoPId)

    Debug.Log("选择界面完成，切换场景。。")
    SceneTool.ChangeScene(_M.sceneList[_M.sceneIndex])
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
function _M.PlayerSelectChange(id, b, heroId)
    if id == GlobalDefine.OnePId then
        _M.isOnePSelect = b
        _M.onePSelectId = heroId
    else
        _M.isTwoPSelect = b
        _M.twoPSelectId = heroId
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

function _M.LeftBtnClick()
    Debug.Log("LeftClick")
    _M.sceneIndex = _M.sceneIndex - 1
    if _M.sceneIndex == 0 then
        _M.sceneIndex = #_M.sceneList
    end
    _M.RefreshScene()
end

function _M.RightBtnClick()
    _M.sceneIndex = _M.sceneIndex + 1
    if _M.sceneIndex == #_M.sceneList + 1 then
        _M.sceneIndex = 1
    end
    _M.RefreshScene()
end

function _M.RefreshScene()
    if _M.sceneIndex == nil then
        _M.sceneIndex = 1
    end
    Debug.Log("refrsh  index==" .. tostring(_M.sceneIndex) .. ", count==" .. tostring(#_M.sceneList))
    local sceneId = _M.sceneList[_M.sceneIndex]
    _M.sceneNameText.text = Scene[sceneId].name
end

return _M