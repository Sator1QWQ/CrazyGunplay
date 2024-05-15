/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_SkillExpression : IConfigBase
{
	int id { get; set; }

	int nextExpression { get; set; }

	float delayTime { get; set; }

	int animationSkillId { get; set; }

	string audioPath { get; set; }

	int effectId { get; set; }

}