using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using UnityEditor;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class LuaScriptEx
{
    [MenuItem("Assets/Copy Lua Script require Path")]
	private static void GetLuaScriptPath()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if(path.IndexOf("Assets/LuaScripts/") != -1)
        {
            string noAsset = path.Replace("Assets/LuaScripts/", "");
            string noEx = noAsset.Replace(".lua", "");
            GUIUtility.systemCopyBuffer = noEx;
            Debug.Log("copy path==" + noEx);
        }

    }
}
