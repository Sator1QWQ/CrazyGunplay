﻿using System.Collections;
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

	public const float DESIGN_WIDTH = 1920;	//设计宽
	public const float DESIGN_HEIGHT = 1080;    //设计高

	/// <summary>
	/// 重力加速度
	/// </summary>
	public static readonly Vector3 G = new Vector3(0, -15f, 0);
}
