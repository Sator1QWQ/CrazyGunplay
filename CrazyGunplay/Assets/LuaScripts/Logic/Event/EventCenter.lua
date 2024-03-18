EventCenter = Class.Create("EventCenter", Object)

function EventCenter:ctor()
    self.eventTable = {}
end

--注册监听
function EventCenter:AddListener(eventId, event)
    if self.eventTable[eventId] == nil then
        self.eventTable[eventId] = {}
    end

    table.insert(self.eventTable[eventId], event) 
end

--发送广播
function EventCenter:Send(eventId, ...)
    local args = {...}
    if self.eventTable[eventId] == nil then
        Debug.LogError("Lua 没注册eventId！ eventId==" .. tostring(eventId))
        return
    end

    for _, v in ipairs(self.eventTable[eventId]) do
        v(args)
    end
end 

--清除所有事件
function EventCenter:ClaerEvent()
    self.eventTable = {}
end

EventCenter.Instance = EventCenter.new()