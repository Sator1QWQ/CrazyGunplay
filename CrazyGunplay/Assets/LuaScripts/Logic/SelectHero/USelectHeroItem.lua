require "Configs.Config.Character"

local _M = {}

function _M.ItemInit(item, index)
    _M.index = index
    local id = 1000 + index + 1
    item:Get("name_text").text = Character[id].name
    
    local trigger = EventTrigger.Entry()
    trigger.eventID = EventTriggerType.PointerClick
    trigger.callback = EventTrigger.TriggerEvent()
    trigger.callback:AddListener(_M.OnClick)
    item:Get("empty_eve").triggers:Add(trigger)
end

function _M.OnClick()
    Debug.Log("_M.index==" .. tostring(_M.index))
    local id = 1000 + _M.index + 1
    MHero.SetSelectPlayer(id)
end

return _M