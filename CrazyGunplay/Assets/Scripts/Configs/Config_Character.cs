/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Character : IConfigBase
{
	int id { get; set; }

	string name { get; set; }

	int initWeapon { get; set; }

	int skillGroup { get; set; }

	string model { get; set; }

	string desc { get; set; }

}