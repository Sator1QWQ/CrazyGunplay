UnityEngine = CS.UnityEngine
Debug = UnityEngine.Debug
UI = UnityEngine.UI
Vector3 = UnityEngine.Vector3
EventSystems = UnityEngine.EventSystems
EventTrigger = EventSystems.EventTrigger
EventTriggerType = EventSystems.EventTriggerType
Time = UnityEngine.Time
Input = UnityEngine.Input
KeyCode = UnityEngine.KeyCode
RectTransform = UnityEngine.RectTransform

Module = CS.Module
UI = Module.UI
Timer = Module.Timer
ReferencePool = CS.GameFramework.ReferencePool
--事件
GFRuntime = CS.UnityGameFramework.Runtime
PlayerDieEventArgs = CS.PlayerDieEventArgs

require "Base.Class"
require "Global.GlobalDefine"
require "Global.GlobalEnum"
Text = require "Global.Text"

require "Base.ControllerRequire"
require "Base.ModelRequire"
require "Base.ToolRequire"
require "Logic.GameMode.ModeFactory"
require "Logic.GameMode.GameModeBase"
require "Logic.GameMode.LocalMode"

Debug.Log("Lua Main运行!!!")