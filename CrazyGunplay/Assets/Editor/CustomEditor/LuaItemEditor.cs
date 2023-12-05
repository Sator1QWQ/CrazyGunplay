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
		LuaItem扩展
*/
[CustomEditor(typeof(LuaItem))]
public class LuaItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("生成挂载对象"))
        {
            LuaItem item = (serializedObject.targetObject as LuaItem);
            List<GameObject> uiList = item.uiList;
            Transform[] tfs = item.GetComponentsInChildren<Transform>();
            if (uiList == null)
            {
                uiList = new List<GameObject>();
            }
            uiList.Clear();
            for (int i = 0; i < tfs.Length; i++)
            {
                string objName = tfs[i].name;
                //_为特殊符号
                if (objName.IndexOf('_') != -1)
                {
                    uiList.Add(tfs[i].gameObject);
                }
            }
        }
        base.OnInspectorGUI();
    }
}
