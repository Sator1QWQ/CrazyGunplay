require "Configs.Config.TeamConfig"
local _M = {}

function _M.OnInit(panel)
    _M.panel = panel
    panel:Get("Back_btn").onClick:AddListener(_M.OnClickBack)
end

function _M.OnOpen()
    local winnerTeam = BattleModeScene.Instance.winnerTeam
    --没有winnerTeam，表示平局
    if winnerTeam then
        local teamName = TeamConfig[winnerTeam].name
        _M.panel:Get("Victory_text").text = string.format(Text.Victory, teamName)
    else
        _M.panel:Get("Victory_text").text = Text.DeadHeat
    end
    
end

function _M.OnClickBack()
    BattleModeScene.Instance:ClearBattleData()

    CS.SceneTool.UnloadAllScene()
    --Module.Scene:UnloadScene("Assets/Resource/Scenes/Battle_1.unity")
    _M.panel:Close()
    UI:OpenUIForm("Assets/Resource/UI/StartPanel.prefab", "NormalGroup")
    UI:OpenUIForm("Assets/Resource/UI/SelectHeroPanel.prefab", "NormalGroup")
end

return _M