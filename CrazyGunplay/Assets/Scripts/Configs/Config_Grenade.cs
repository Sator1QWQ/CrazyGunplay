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

	int areaId { get; set; }

	int targetId { get; set; }

	int mainMag { get; set; }

	float fireRate { get; set; }

	float delay { get; set; }

	float throwTime { get; set; }

	List<int> buffIdList { get; set; }

	List<float> buffValueList { get; set; }

	List<float> buffContinueList { get; set; }

	string effectPath { get; set; }

}