using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public interface Config_Bullet : IConfigBase
{
	int id { get; set; }

	BulletType bulletType { get; set; }

	List<int> bulletParams { get; set; }

	int bulletCount { get; set; }

	List<float> bulletOffset { get; set; }

	float flySpeed { get; set; }

	bool canHitSelf { get; set; }

	BulletHideType bulletHideType { get; set; }

	string assetPath { get; set; }

	string effectPath { get; set; }

}