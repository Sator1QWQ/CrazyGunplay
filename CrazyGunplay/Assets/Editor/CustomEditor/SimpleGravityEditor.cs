using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleGravity))]
public class SimpleGravityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(Application.isPlaying)
        {
            SimpleGravity g = serializedObject.targetObject as SimpleGravity;
            if (g.GetComponent<SimpleGravityGizmos>() == null)
            {
                g.gameObject.AddComponent<SimpleGravityGizmos>();
                Debug.Log("AddCOmp");
            }
        }
    }
}
