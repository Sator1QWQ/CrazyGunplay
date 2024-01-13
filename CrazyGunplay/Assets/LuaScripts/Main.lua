UnityEngine = CS.UnityEngine
Debug = UnityEngine.Debug
UI = UnityEngine.UI
Vector3 = UnityEngine.Vector3
EventSystems = UnityEngine.EventSystems
EventTrigger = EventSystems.EventTrigger
EventTriggerType = EventSystems.EventTriggerType
Time = UnityEngine.Time

UI = CS.Module.UI
Timer = CS.Module.Timer

require "Base.Class"
require "Global.GlobalDefine"
require "Global.GlobalEnum"
Text = require "Global.Text"

require "Base.ControllerRequire"
require "Base.ModelRequire"
require "Base.ToolRequire"

Debug.Log("Lua Main运行!!!")