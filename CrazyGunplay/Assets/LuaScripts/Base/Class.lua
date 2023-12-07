Class = {}
Class.classDic = {}

--创建类，必须带名字，可以加父类
function Class.Create(name, parent)
    local table = {}
    table.name = name
    table.base = parent
    function table.new()
        local obj = {}
        setmetatable(obj, table)
        if obj.ctor ~= nil then
            obj:ctor()
        end
        return obj
    end
    table.__index = table
    if parent ~= nil then
        setmetatable(table, parent)
        Class.classDic[name] = {table = table, parent = parent.name}
    else
        Class.classDic[name] = {table = table, parent = nil}
    end
    return table
end

Object = Class.Create("Object")
function Object:Dispose()
    Debug.Log("销毁Object对象")
end