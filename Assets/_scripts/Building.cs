using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;





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

    public int GoldPrice
    {
        get
        {
            return gold;
        }
    }

    public int WoodPrice
    {
        get
        {
            return wood;
        }
    }

    public int StonePrice
    {
        get
        {
            return stone;
        }
    }

    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }
    }
}
