using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class GlobalDefine
{
	/// <summary>
	/// lua脚本路径
	/// </summary>
	public static readonly string LUA_PATH = Application.dataPath + "/LuaScripts/";

	/// <summary>
	/// ui界面路径
	/// </summary>
	public static readonly string PANEL_PATH = "Assets/Resource/UI/";

	/// <summary>
	/// 场景路径
	/// </summary>
	public static readonly string SCENE_PATH = "Assets/Resource/Scenes/";

	public static readonly string PLAYER_MODEL_PATH = "Assets/Resource/Models/Player/";

	public const float DESIGN_WIDTH = 1920;	//设计宽
	public const float DESIGN_HEIGHT = 1080;    //设计高

	/// <summary>
	/// 重力加速度
	/// </summary>
	public static readonly Vector3 G = new Vector3(0, -15f, 0);

	/// <summary>
	/// 玩家身高
	/// </summary>
	public static float PLAYER_HEIGHT = 0.5f;

	/// <summary>
	/// 地面射线检测的长度
	/// </summary>
	public static float FLOOR_RAY_DISTANCE = 0.1f;

	/// <summary>
	/// dush持续时间
	/// </summary>
	public static float DUSH_TIME = 0.1f;

	/// <summary>
	/// 远离场景
	/// </summary>
	public static Vector3 FAY_WAY = Vector3.right * 1000;
}
