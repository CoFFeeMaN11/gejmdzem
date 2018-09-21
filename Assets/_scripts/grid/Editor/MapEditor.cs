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
        
    }

    private void OnSceneGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            Vector2 mousePos = Event.current.mousePosition;
            mousePos.y = Camera.current.pixelHeight - mousePos.y;
            Debug.Log(Camera.current.ScreenPointToRay(mousePos));
        }
    }

}
