local _M = {}

function _M.OnInit(panel)
    panel:Get("Start_text").text = Text.StartText1
    panel:Get("Setting_text").text = Text.SettingText1
    panel:Get("Quit_text").text = Text.QuitText1
    panel:Get("Start_btn").onClick:AddListener(function()
        UI:OpenUIForm("Assets/Resource/UI/GameModePanel.prefab", "NormalGroup")
    end)

    ModeFactory.Instance:Init()
end

return _M