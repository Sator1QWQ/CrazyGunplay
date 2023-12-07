require "Configs.Config.Character"
require "Logic.GameMode.ModeFactory"

local _M = {}

function _M.ItemInit(item, index)
    _M.index = index
    local heroId = 1000 + index + 1
    item:Get("name_text").text = Character[heroId].name
    
    local trigger = EventTrigger.Entry()
    trigger.eventID = EventTriggerType.PointerClick
    trigger.callback = EventTrigger.TriggerEvent()
    trigger.callback:AddListener(function()
        _M.Click(item)
    end)
    item:Get("empty_eve").triggers:Add(trigger)
    
    item:Get("1P_text").text = Text.OneP
    item:Get("2P_text").text = Text.TwoP
    _M.onePSelectGo = item:GetGameObject("1PSelect_go")
    _M.twoPSelectGo = item:GetGameObject("2PSelect_go")
    _M.onePSelectGo:SetActive(false)
    _M.twoPSelectGo:SetActive(false)
end

function _M.OnSelect(isSelect)
    Debug.Log("OnSelect==" .. tostring(_M.index) .. ", isSelect==" .. tostring(isSelect))
    local heroId = 1000 + _M.index + 1
    
    --1P已经被选中了
    if MHero.Instance.selectHeroDic[1] ~= nil then
        _M.onePSelectGo:SetActive(MHero.Instance.selectHeroDic[1] == heroId)
        _M.twoPSelectGo:SetActive(isSelect)
    else
        _M.onePSelectGo:SetActive(isSelect)
        _M.twoPSelectGo:SetActive(false)
    end
end

function _M.Click(item)
    local isSelectOneP = MHero.Instance.selectHeroDic[1] ~= nil
    local isSelectTwoP = MHero.Instance.selectHeroDic[2] ~= nil
    if isSelectOneP and isSelectTwoP then
        return
    end

    item.behaviour.parent.gameObject:GetComponent(typeof(CS.LuaPanel)):Get("CharacterGrid_grid"):Select(_M.index)
end

return _M