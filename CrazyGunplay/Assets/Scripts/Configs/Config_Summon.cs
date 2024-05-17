/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Summon : IConfigBase
{
	int id { get; set; }

	int spawnCount { get; set; }

	float spawnInterval { get; set; }

	float moveSpeed { get; set; }

	bool isContinueFollow { get; set; }

	bool isRandomStartPoint { get; set; }

	bool isRandomRoute { get; set; }

	string assetPath { get; set; }

}