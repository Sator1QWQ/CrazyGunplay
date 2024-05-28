/***********************代码由工具生成***********************/
using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Weapon : IConfigBase
{
	int id { get; set; }

	string name { get; set; }

	WeaponType weaponType { get; set; }

	int targetId { get; set; }

	GetHitType beatType { get; set; }

	float beatBack { get; set; }

	WeaponAnimType animType { get; set; }

	string path { get; set; }

}