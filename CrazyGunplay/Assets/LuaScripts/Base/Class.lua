Class = {}
Class.objDic = {}

--创建类，必须带名字，可以加父类
function Class.Create(name, parent)
    local table = {}
    table.name = name
    function table.new()
        local obj = {}
        setmetatable(obj, table)
        return obj
    end
    table.__index = table
    if parent ~= nil then
        setmetatable(table, parent)
        Class.objDic[name] = {table = table, parent = parent.name}
    else
        Class.objDic[name] = {table = table, parent = nil}
    end
    return table
end