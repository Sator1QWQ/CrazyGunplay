require "Configs.Config.Character"
--require "Logic.GameMode.ModeFactory"
local USelectHero = require "Logic.SelectHero.USelectHero"

--所有Item必须以通过Class创建
local USelectHeroItem = Class.Create("USelectHeroItem", Object)

function USelectHeroItem:ItemInit(luaItem, index)
    self.index = index
    local heroId = 1000 + index + 1
    luaItem:Get("name_text").text = Character[heroId].name
    luaItem:Get("1P_text").text = Text.OneP
    luaItem:Get("2P_text").text = Text.TwoP
    self.onePSelectGo = luaItem:GetGameObject("1PSelect_go")
    self.twoPSelectGo = luaItem:GetGameObject("2PSelect_go")
    self.onePSelectGo:SetActive(false)
    self.twoPSelectGo:SetActive(false)
    self.luaItem = luaItem
end

function USelectHeroItem:OnOpen()
    self.onePSelectGo:SetActive(false)
    self.twoPSelectGo:SetActive(false)
    self:InitSelect()
end

function USelectHeroItem:InitSelect()
    self.luaItem:InitSelect()
end

function USelectHeroItem:OnSelect(isSelect)
    local curSelectPlayer

    --1P已经被选中了
    if USelectHero.isOnePReady then
        self.twoPSelectGo:SetActive(isSelect)
        curSelectPlayer = GlobalDefine.TwoPId
    else
        self.onePSelectGo:SetActive(isSelect)
        self.twoPSelectGo:SetActive(false)
        curSelectPlayer = GlobalDefine.OnePId
    end

    USelectHero.PlayerSelectChange(curSelectPlayer, isSelect)
    USelectHero.Refresh()
end

return USelectHeroItem