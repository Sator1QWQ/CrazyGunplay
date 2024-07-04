require "Configs.Config.WeaponAsset"

--本地模式下的战斗界面
local _M = {}
function _M.OnInit(panel)
    local grid = panel:Get("Player_grid")
    --数量为全部角色
    grid:SetCount(MPlayer.Instance.count)
    _M.grid = grid
    _M.panel = panel

    --1p
    _M.onePWeaponIcon = panel:Get("IconOne_img")
    _M.onePWeaponName = panel:Get("WeaponNameOne_text")
    _M.onePBulletText = panel:Get("BulletOne_text")

    _M.twoPWeaponIcon = panel:Get("IconTwo_img")
    _M.twoPWeaponName = panel:Get("WeaponNameTwo_text")
    _M.twoPBulletText = panel:Get("BulletTwo_text")

    _M.RefreshUI()
end

function _M.OnOpen(panel)
    Module.Event:Subscribe(CS.HitEventArgs.EventId, _M.BulletHitEvent)
    Module.Event:Subscribe(CS.ChangeWeaponEventArgs.EventId, _M.OnChangeWeapon)
    Module.Event:Subscribe(CS.BulletCountChangeEventArgs.EventId, _M.OnChangeBulletCount)
    Module.Event:Subscribe(CS.WeaponReloadFinishEventArgs.EventId, _M.OnWeaponReloadFinish)
end

function _M.OnClose()
    Module.Event:Unsubscribe(CS.HitEventArgs.EventId, _M.BulletHitEvent)
    Module.Event:Unsubscribe(CS.ChangeWeaponEventArgs.EventId, _M.OnChangeWeapon)
    Module.Event:Unsubscribe(CS.BulletCountChangeEventArgs.EventId, _M.OnChangeBulletCount)
    Module.Event:Unsubscribe(CS.WeaponReloadFinishEventArgs.EventId, _M.OnWeaponReloadFinish)
end

function _M.BulletHitEvent(sender, args)
    _M.grid:Foreach(function(item)
        item:RefreshBeatBackPercent()
    end)
end

function _M.OnChangeWeapon(sender, args)
    _M.RefreshWeaponUI(args.PlayerId)
end

function _M.OnChangeBulletCount(sender, args)
    _M.RefreshWeaponUI(args.PlayerId)
end

function _M.OnWeaponReloadFinish(sender, args)
    _M.RefreshWeaponUI(args.PlayerId)
end

function _M.RefreshUI()
    _M.RefreshWeaponUI(GlobalDefine.OnePId)
    _M.RefreshWeaponUI(GlobalDefine.TwoPId)
end

--刷新武器ui
function _M.RefreshWeaponUI(playerId)
    local playerEntity = Module.PlayerData:GetPlayerEntity(playerId)
    local weapon = playerEntity.WeaponManager.CurrentWeapon
    local weaponId = weapon.Id
    local weaponIcon
    local weaponNameText
    local bulletText
    if playerId == GlobalDefine.OnePId then
        weaponIcon = _M.onePWeaponIcon
        weaponNameText = _M.onePWeaponName
        bulletText = _M.onePBulletText
    else
        weaponIcon = _M.twoPWeaponIcon
        weaponNameText = _M.twoPWeaponName
        bulletText = _M.twoPBulletText
    end

    weaponNameText.text = Weapon[weaponId].name
    local weaponType = Weapon[weaponId].weaponType
    if weaponType == GlobalEnum.WeaponType.Gun then
        bulletText.text = tostring(weapon.MainMag) .. "/" .. tostring(weapon.SpareMag)
    elseif weaponType == GlobalEnum.WeaponType.Throw then
        bulletText.text = tostring(weapon.MainMag)
    end
    
    local spritePath = WeaponAsset[weaponId].spriteName
    Module.Atlas:SetSprite(weaponIcon, GlobalDefine.AtlasPath .. "BattleCommon/Weapons_1.spriteatlas", spritePath, GlobalDefine.WeaponIconHeight)
end

return _M