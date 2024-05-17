/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_HitArea : IConfigBase
{
	int id { get; set; }

	AreaType areaType { get; set; }

	List<float> value { get; set; }

	List<float> startPointOffset { get; set; }

}