/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Grenade : IConfigBase
{
	int id { get; set; }

	string desc { get; set; }

	int bulletId { get; set; }

	bool isSkill { get; set; }

	GrenadeRangeType rangeType { get; set; }

	float radius { get; set; }

	float fireRate { get; set; }

	float delay { get; set; }

	int buffId { get; set; }

	float buffValue { get; set; }

	float buffDuration { get; set; }

	string effectPath { get; set; }

}