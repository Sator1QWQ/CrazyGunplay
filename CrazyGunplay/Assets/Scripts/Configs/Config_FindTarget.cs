/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_FindTarget : IConfigBase
{
	int id { get; set; }

	TargetType targetType { get; set; }

	TargetSelectMode targetMode { get; set; }

	CompareType compare { get; set; }

	float targetValue { get; set; }

	int targetNum { get; set; }

}