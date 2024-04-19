using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Gun : IConfigBase
{
	int id { get; set; }

	string desc { get; set; }

	int bulletId { get; set; }

	float fireRate { get; set; }

	float range { get; set; }

	float reloadTime { get; set; }

	int mainMag { get; set; }

	int spareMag { get; set; }

	float perRecoil { get; set; }

	float maxRecoil { get; set; }

}