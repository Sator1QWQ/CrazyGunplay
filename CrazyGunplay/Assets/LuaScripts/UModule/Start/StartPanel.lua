local _M = {}

function _M.OnInit(panel)
    panel:Get("Start_text").text = Text.StartText1
    panel:Get("Setting_text").text = Text.SettingText1
    panel:Get("Quit_text").text = Text.QuitText1
end

return _M