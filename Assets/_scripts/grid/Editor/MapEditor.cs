using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor {

    public override void OnInspectorGUI()
    {
        Map map = (Map)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Map"))
        {
            map.CreateGrid();

        }
        if (GUILayout.Button("Reload"))
        {
            map.Recovery();

        }
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

}
