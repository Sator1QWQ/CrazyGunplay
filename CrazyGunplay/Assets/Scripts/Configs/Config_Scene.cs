/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Scene : IConfigBase
{
	int id { get; set; }

	string name { get; set; }

	int sceneType { get; set; }

	string assetPath { get; set; }

}