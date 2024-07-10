/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_PreloadConfig : IConfigBase
{
	int id { get; set; }

	PreloadAssetType assetType { get; set; }

	PreloadLogic loadLogic { get; set; }

	List<string> loadValues { get; set; }

}