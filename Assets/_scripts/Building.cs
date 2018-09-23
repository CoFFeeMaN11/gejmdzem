using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnumFlagsAttribute : PropertyAttribute
{
    public EnumFlagsAttribute() { }
}

[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _position, UnityEditor.SerializedProperty _property, GUIContent _label)
    {
        // Change check is needed to prevent values being overwritten during multiple-selection
        UnityEditor.EditorGUI.BeginChangeCheck();
        int newValue = UnityEditor.EditorGUILayout.MaskField(_property.intValue, _property.enumNames);
        if (UnityEditor.EditorGUI.EndChangeCheck())
        {
            _property.intValue = newValue;
        }
    }
}

[System.Flags]
public enum TerrainType
{
    GRASS = (1<<0),
    ROAD = (1<<1),
    FOREST = (1<<2),
    ROCKS = (1<<3),
}

[CreateAssetMenu(fileName = "Building", menuName = "Building", order = 1)]
public class Building : ScriptableObject
{
    [SerializeField]
    private int id;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Building[] requiments;
    [SerializeField][EnumFlags]
    private TerrainType canBuildOn;
    [Header("Price")]
    [SerializeField]
    private int gold;
    [SerializeField]
    private int wood;
    [SerializeField]
    private int stone;

    protected int GoldPrice
    {
        get
        {
            return gold;
        }
    }

    protected int WoodPrice
    {
        get
        {
            return wood;
        }
    }

    protected int StonePrice
    {
        get
        {
            return stone;
        }
    }

    protected GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }
}
