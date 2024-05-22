/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_SkillActionTree : IConfigBase
{
	int id { get; set; }

	SkillCastAction actionType { get; set; }

	List<object> actionValue { get; set; }

	ActionConditionType conditionType { get; set; }

	float delayTime { get; set; }

	float continueTime { get; set; }

	int areaId { get; set; }

	ActionAreaTriggerTiming areaTriggerType { get; set; }

	int areaTriggerCount { get; set; }

	float areaTriggerInterval { get; set; }

	List<int> nextActionList { get; set; }

}