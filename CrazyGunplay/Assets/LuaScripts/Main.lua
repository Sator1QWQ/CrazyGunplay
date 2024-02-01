UnityEngine = CS.UnityEngine
Debug = UnityEngine.Debug
UI = UnityEngine.UI
Vector3 = UnityEngine.Vector3
EventSystems = UnityEngine.EventSystems
EventTrigger = EventSystems.EventTrigger
EventTriggerType = EventSystems.EventTriggerType
Time = UnityEngine.Time

Module = CS.Module
UI = Module.UI
Timer = Module.Timer

require "Base.Class"
require "Global.GlobalDefine"
require "Global.GlobalEnum"
Text = require "Global.Text"

require "Base.ControllerRequire"
require "Base.ModelRequire"
require "Base.ToolRequire"

Debug.Log("Lua Main运行!!!")