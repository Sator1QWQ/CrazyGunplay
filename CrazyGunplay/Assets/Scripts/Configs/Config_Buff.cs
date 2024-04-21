using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Buff : IConfigBase
{
	int id { get; set; }

	BuffType type { get; set; }

	float triggerInterval { get; set; }

	string desc { get; set; }

	string effectPath { get; set; }

	string iconPath { get; set; }

}