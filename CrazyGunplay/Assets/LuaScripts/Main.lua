UnityEngine = CS.UnityEngine
Debug = UnityEngine.Debug

Debug.Log("Lua Hello!!!")

local _M = {}

function _M:Awake()
    Debug.Log("Lua Awake!")
end

return _M