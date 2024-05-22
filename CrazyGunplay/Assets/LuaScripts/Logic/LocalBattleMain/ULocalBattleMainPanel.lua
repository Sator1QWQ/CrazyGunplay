--本地模式下的战斗界面
local _M = {}
function _M.OnInit(panel)
    local grid = panel:Get("Player_grid")
    --数量为全部角色
    grid:SetCount(MPlayer.Instance.count)
    _M.grid = grid
   
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
return _M