/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_SkillActionTree : IConfigBase
{
	int id { get; set; }

	int actionType { get; set; }

	List<object> actionValue { get; set; }

	int conditionType { get; set; }

	List<int> nextActionList { get; set; }

}