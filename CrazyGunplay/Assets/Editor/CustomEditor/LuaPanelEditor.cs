﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using UnityEditor;

/*
* 作者：
* 日期：
* 描述：
		LuaPanel扩展
*/
[CustomEditor(typeof(LuaPanel))]
public class LuaPanelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("生成挂载对象"))
        {
            LuaPanel panel = (serializedObject.targetObject as LuaPanel);
            List<GameObject> uiList = panel.uiList;
            Transform[] tfs = panel.GetComponentsInChildren<Transform>();
            uiList.Clear();
            for (int i = 0; i < tfs.Length; i++)
            {
                string objName = tfs[i].name;
                if (objName[objName.Length - 1].Equals('_'))
                {
                    uiList.Add(tfs[i].gameObject);
                }
            }
        }
        base.OnInspectorGUI();
    }
}
