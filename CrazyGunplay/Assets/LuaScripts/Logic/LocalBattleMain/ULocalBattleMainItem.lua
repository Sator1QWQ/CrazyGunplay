require "Configs.Config.Weapon"

local ULocalBattleMainItem = Class.Create("ULocalBattleMainItem", Object)

function ULocalBattleMainItem:ItemInit(luaItem, index)
    self.luaIndex = index + 1
    local playerInfo = MPlayer.Instance:GetPlayerByIndex(self.luaIndex)
    luaItem:Get("PlayerName_text").text = playerInfo.name
    luaItem:Get("PercentLabel_text").text = Text.PercentLabel
    luaItem:Get("Percent_text").text = Text.PercentZero
    self.luaItem = luaItem

    --不能在item中使用Subscribe，因为不同item的函数是同一个引用，不允许重复注册
    --不管使不使用self都是一样
    --Module.Event:Subscribe(CS.HitEventArgs.EventId, self.BulletHitEvent)
end

function ULocalBattleMainItem:RefreshBeatBackPercent()
    local playerInfo = MPlayer.Instance:GetPlayerByIndex(self.luaIndex)
    local value = playerInfo.beatBackPercent
    self.luaItem:Get("Percent_text").text = string.format(Text.Percent, value)
end

return ULocalBattleMainItem