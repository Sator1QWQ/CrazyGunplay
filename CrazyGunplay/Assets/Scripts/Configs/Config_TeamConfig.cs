using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_TeamConfig : IConfigBase
{
	int id { get; set; }

	string name { get; set; }

}