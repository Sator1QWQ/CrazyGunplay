/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Skill : IConfigBase
{
	int id { get; set; }

	string name { get; set; }

	SkillType type { get; set; }

	bool isBigSkill { get; set; }

	int skillActionId { get; set; }

	int findTargetId { get; set; }

	float beforeCast { get; set; }

	float afterCast { get; set; }

	List<int> activeCondition { get; set; }

	float skillDuration { get; set; }

	SkillCoolingTiming coolingTiming { get; set; }

	float coolingTime { get; set; }

	List<int> expressionList { get; set; }

	int canUseNum { get; set; }

}