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

	SkillCastAction mode { get; set; }

	List<int> values { get; set; }

	TargetType castTarget { get; set; }

	TargetSelectMode targetMode { get; set; }

	CompareType compare { get; set; }

	float targetValue { get; set; }

	int targetNum { get; set; }

	float beforeCast { get; set; }

	float afterCast { get; set; }

	List<int> activeCondition { get; set; }

	float skillDuration { get; set; }

	float coolingTime { get; set; }

	int canUseNum { get; set; }

}