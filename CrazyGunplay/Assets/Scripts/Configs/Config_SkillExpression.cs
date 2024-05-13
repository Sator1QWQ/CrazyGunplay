/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_SkillExpression : IConfigBase
{
	int id { get; set; }

	float delayTime { get; set; }

	string animationPath { get; set; }

	string audioPath { get; set; }

	string effectPath { get; set; }

	bool isEffectFollow { get; set; }

	List<float> effectScale { get; set; }

	SkillEffectDeleteTiming effectDeleteTiming { get; set; }

}