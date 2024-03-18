local _M = {}

function _M.OnInit(panel)
    _M.panel = panel
    panel:Get("Victory_text").text = string.format(Text.Victory, "红")
    panel:Get("Back_btn").onClick:AddListener(_M.OnClickBack)
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