/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Effect : IConfigBase
{
	int id { get; set; }

	string effectPath { get; set; }

	bool isEffectFollow { get; set; }

	SkillEffectDeleteTiming effectDeleteTiming { get; set; }

}