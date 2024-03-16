
local _M = {}

function _M.OnInit(panel)
    local trigger = EventTrigger.Entry()
    trigger.eventID = EventTriggerType.PointerClick
    trigger.callback = EventTrigger.TriggerEvent()
    trigger.callback:AddListener(function()
        panel:Close()
    end)
    panel:Get("blackBg_eve").triggers:Add(trigger)
    panel:Get("Local_btn").onClick:AddListener(_M.Local)
end

--本地按钮
function _M.Local()
    --MSelectHero.Instance:SetMode(GlobalEnum.GameMode.Local)
    UI:OpenUIForm("Assets/Resource/UI/SelectHeroPanel.prefab", "NormalGroup")
end

return _M