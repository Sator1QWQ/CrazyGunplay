using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Skill : IConfigBase
{
	int id { get; set; }

	string name { get; set; }

	SkillType type { get; set; }

	bool isBigSkill { get; set; }

	SkillCastMode mode { get; set; }

	List<int> values { get; set; }

	SkillTargetType castTarget { get; set; }

	List<int> activeCondition { get; set; }

	float skillDuration { get; set; }

	float coolingTime { get; set; }

	int canUseNum { get; set; }

}