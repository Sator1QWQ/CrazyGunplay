require "Configs.Config.WeaponAsset"

--本地模式下的战斗界面
local _M = {}
function _M.OnInit(panel)
    local grid = panel:Get("Player_grid")
    --数量为全部角色
    grid:SetCount(MPlayer.Instance.count)
    _M.grid = grid

    --1p
    
    panel:Get("WeaponNameOne_text")
    panel:Get("BulletOne_text")
    _M.panel = panel
    _M.RefreshUI()
end

function _M.OnOpen(panel)
    Module.Event:Subscribe(CS.HitEventArgs.EventId, _M.BulletHitEvent)
end

function _M.OnClose()
    Module.Event:Unsubscribe(CS.HitEventArgs.EventId, _M.BulletHitEvent)
end

function _M.BulletHitEvent(sender, args)
    _M.grid:Foreach(function(item)
        item:RefreshBeatBackPercent()
    end)
end

function _M.RefreshUI()
    _M.RefreshWeaponUI(GlobalDefine.OnePId)
    _M.RefreshWeaponUI(GlobalDefine.TwoPId)
end

function _M.RefreshWeaponUI(playerId)
    local playerEntity = Module.PlayerData:GetPlayerEntity(playerId)
    local weaponId = playerEntity.WeaponManager.CurrentWeapon.Id
    local img 
    if playerId == GlobalDefine.OnePId then
        img = _M.panel:Get("IconOne_img")
    else
        img = _M.panel:Get("IconTwo_img")
    end

    local spritePath = WeaponAsset[weaponId].spriteName
    Module.Atlas:SetSprite(img, GlobalDefine.AtlasPath .. "BattleCommon/Weapons_1.spriteatlas", spritePath)
end

return _M