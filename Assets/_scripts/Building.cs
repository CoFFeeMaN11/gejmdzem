using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum TerrainType
{
    GRASS,
    ROAD,
    FOREST,
    ROCKS,
}
public class EnumFlagsAttribute : PropertyAttribute
{
    public EnumFlagsAttribute() { }
}

[CreateAssetMenu(fileName = "Tower", menuName = "Building", order = 1)]
public abstract class Building : ScriptableObject
{
    [SerializeField]
    protected int maxHP;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    protected Building[] requiments;
    [SerializeField]
    protected TileCoords coords;
    [SerializeField][EnumFlags]
    protected TerrainType canBuildOn;
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
}
