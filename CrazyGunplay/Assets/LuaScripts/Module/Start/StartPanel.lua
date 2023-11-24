local _M = {}

function _M.Awake()
    Debug.Log("StartPanel 初始化")
end

function _M.OnInit(obj)
    Debug.Log("obj==" .. tostring(obj))
end

return _M