using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum TileEditorType
{
    NONE,
    STANDARD,
    FOREST,
    ROCKS,
}

public class MapEditorWindow : EditorWindow {

    TileEditorType currentMode = TileEditorType.NONE;
    private static GUIStyle ToggleButtonStyleNormal = null;
    private static GUIStyle ToggleButtonStyleToggled = null;
    private bool blockingMouseInput;
    Map editedMap;
    Road editedRoad;
    [MenuItem("Window/Map editor")]
    public static void ShowWindow()
    {
        GetWindow<MapEditorWindow>("Map Editor");
    }

    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += SceneGUI;
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Map");
        editedMap = EditorGUILayout.ObjectField(editedMap, typeof(Map), true) as Map;
        if (editedMap == null) return;
        GUILayout.EndHorizontal();
        if (ToggleButtonStyleNormal == null)
        {
            ToggleButtonStyleNormal = "Button";
            ToggleButtonStyleToggled = new GUIStyle(ToggleButtonStyleNormal);
            ToggleButtonStyleToggled.normal.background = ToggleButtonStyleToggled.active.background;
        }
        if (GUILayout.Button("None", currentMode == TileEditorType.NONE ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            currentMode = TileEditorType.NONE;
        }
        if (GUILayout.Button("Standard", currentMode == TileEditorType.STANDARD ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            currentMode = TileEditorType.STANDARD;
        }
        if (GUILayout.Button("Forest", currentMode == TileEditorType.FOREST ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            currentMode = TileEditorType.FOREST;
        }
        if (GUILayout.Button("Rocks", currentMode == TileEditorType.ROCKS ? ToggleButtonStyleToggled : ToggleButtonStyleNormal))
        {
            currentMode = TileEditorType.ROCKS;
        }

        if (GUILayout.Button("Reset Map"))
        {
            editedMap.ResetMap();
        }
        if (GUILayout.Button("Test"))
        {
            GameManagerScript.GetAllBuilding();
        }
        GUILayout.Label("Roads", EditorStyles.boldLabel);
        if (GUILayout.Button("Add road"))
        {
            editedRoad = new GameObject("Road").AddComponent<Road>();
            editedRoad.transform.position = Vector3.zero;
            for (int i = 0; i < 3; i++)
                editedRoad.AddWaypoint(Vector3.zero, editedMap);
        }
        editedRoad = EditorGUILayout.ObjectField(editedRoad, typeof(Road), true) as Road;
        if (editedRoad != null)
            EditRoad();
    }
        
    void EditRoad()
    {
        if (GUILayout.Button("Add waypoint"))
        { 
            editedRoad.AddWaypoint(editedRoad.WayPoints[editedRoad.WayPoints.Count-1].transform.position, editedMap);
        }
        if (GUILayout.Button("Bake road"))
        {
            editedMap.BakeRoad(editedRoad);
        }
    }

    private void Awake()
    {
        editedMap = GameObject.FindObjectOfType<Map>();
        if (editedMap != null)
            editedMap.Recovery();
    }

    void SceneGUI(SceneView sceneView)
    {
        if (currentMode == TileEditorType.NONE) return;
        Event e = Event.current;

        if (e.type == EventType.MouseDown)
        {
            blockingMouseInput = true;
            PaintMap();

        }
        else if (e.type == EventType.MouseDrag)
        {
            PaintMap();
        }
        else if (e.type == EventType.MouseMove)
        {

        }
        else if (e.type == EventType.MouseUp)
        {
            if (blockingMouseInput)
            {
                e.Use();
                

            }
            blockingMouseInput = false;
        }
        else if (e.type == EventType.Layout)
        {
            //somehow this allows e.Use() to actually function and block mouse input
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(GetHashCode(), FocusType.Passive));
        }
    }

    private void PaintMap()
    {
        Vector2 mousePos = Event.current.mousePosition;
        mousePos.y = Camera.current.pixelHeight - mousePos.y;
        //Debug.Log(Camera.current.ScreenPointToRay(mousePos));
        if (editedMap == null) return;
        TileCoords test = editedMap.FromVector(Camera.current.ScreenPointToRay(mousePos).origin);
        if (!editedMap.Exists(test.x, test.y)) return;
        var tile = editedMap[test.x, test.y];
        SerializedObject serializedObject = new SerializedObject(tile);
        SerializedProperty property = serializedObject.FindProperty("type");
        switch (currentMode)
        {
            case TileEditorType.STANDARD:
                property.intValue = (int)TileType.STANDARD;
                break;
            case TileEditorType.FOREST:
                property.intValue = (int)TileType.FOREST;
                break;
            case TileEditorType.ROCKS:
                property.intValue = (int)TileType.ROCKS;
                break;

        }
        //serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
        SceneView.RepaintAll();
    }
}
