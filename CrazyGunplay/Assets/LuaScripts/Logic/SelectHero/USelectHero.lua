require "Configs.Config.Character"
local _M = {}

function _M.OnInit(panel)
    local grid = panel:Get("CharacterGrid_grid")
    grid:SetCount(Character.Count)
    
end

return _M