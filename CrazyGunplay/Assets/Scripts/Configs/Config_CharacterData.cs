using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_CharacterData : IConfigBase
{
	int id { get; set; }

	float speed { get; set; }

	float jump { get; set; }

	int airJumpNum { get; set; }

	float dushDistance { get; set; }

	int dushNum { get; set; }

}