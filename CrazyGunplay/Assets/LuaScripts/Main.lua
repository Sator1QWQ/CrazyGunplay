UnityEngine = CS.UnityEngine
Debug = UnityEngine.Debug
UI = UnityEngine.UI
Vector3 = UnityEngine.Vector3
EventSystems = UnityEngine.EventSystems
EventTrigger = EventSystems.EventTrigger
EventTriggerType = EventSystems.EventTriggerType

UI = CS.Module.UI
Timer = CS.Module.Timer

require "Base.Class"
GlobalDefine = require "Global.GlobalDefine"
GlobalEnum = require "Global.GlobalEnum"
Text = require "Global.Text"

require "Base.ControllerRequire"
require "Base.ModelRequire"

Debug.Log("Lua Main运行!!!")
require "Global.ModelDefine"