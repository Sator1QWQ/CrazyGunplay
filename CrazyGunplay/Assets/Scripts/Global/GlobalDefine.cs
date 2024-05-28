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
	
	/// <summary>
	/// 玩家模型路径
	/// </summary>

	public static readonly string PLAYER_MODEL_PATH = "Assets/Resource/Models/Player/";

	/// <summary>
	/// 音效路径
	/// </summary>
	public static readonly string AUDIO_PATH = "Assets/Resource/Audio/";

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
	public static float FLOOR_RAY_DISTANCE = 0.2f;

	/// <summary>
	/// 子弹检测长度
	/// </summary>
	public static float BULLET_RAY_DISTANCE = 0.45f;

	/// <summary>
	/// dush持续时间
	/// </summary>
	public static float DUSH_TIME = 0.2f;

	/// <summary>
	/// 远离场景
	/// </summary>
	public static Vector3 FAY_WAY = Vector3.right * 1000;

	public static float HIT_VALUE_REPAIR = 1.0f;

	/// <summary>
	/// 用技能生成的武器槽，槽索引为4
	/// </summary>
	public static int SKILL_WEAPON_SLOT = 4;

	/// <summary>
	/// 地图高度
	/// </summary>
	public static float MAP_MAX_HEIGHT = 16;

	/// <summary>
	/// 被击飞的时间
	/// </summary>
	public static float HIT_FLY_TIME = 2;

	/// <summary>
	/// 限制坐标高度
	/// </summary>
	public static float MIN_Y = -100;
	public static float MAX_Y = 100;
}
