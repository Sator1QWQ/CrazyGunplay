using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_EntityGroupConfig : IConfigBase
{
	int id { get; set; }

	string name { get; set; }

	float instanceAutoReleaseInterval { get; set; }

	int instanceCapacity { get; set; }

	float instanceExpireTime { get; set; }

	int instancePriority { get; set; }

}