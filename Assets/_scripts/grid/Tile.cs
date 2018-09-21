using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    STANDARD,
    ROAD,
    FOREST,
    ROCKS,
    SELECTED,
    BUILDING,
}
[ExecuteInEditMode]
public class Tile : ScriptableObject {
    private int x, y;
    private int size;
    private TileType type;
    private BuildingScript building;

    public static Color[] tileColors =
    {
        new Color32(7,255,19,124),
        new Color32(255,148,53,124),
        new Color32(153,74,41,124),
        new Color32(134,135,138,124),
        new Color32(255,255,255,124)
    };

    public TileType Type
    {
        get
        {
            return type;
        }

        set
        {
            if (value != TileType.BUILDING)
                type = value;
            else
                type = TileType.STANDARD;
        }
    }

    public BuildingScript Building
    {
        get
        {
            return building;
        }
    }

    public void Create(int _x, int _y, int size, TileType _type = TileType.STANDARD)
    {
        x = _x;
        y = _y;
        if (_type == TileType.BUILDING)
            _type = TileType.STANDARD;
    }
    public bool Build(BuildingScript _building)
    {
        if (type == TileType.BUILDING) return false;
        type = TileType.BUILDING;
        building = _building;
        return true;
    }
    public void DrawInEditor(Transform mapTransform)
    {
        Gizmos.color = tileColors[(int)type];
        Gizmos.DrawCube(mapTransform.position + new Vector3((x + 0.5f) * size, (y + 0.5f) * size), new Vector3(size, size));
    }
    public bool Upgrade(BuildingScript _building)
    {
        return true;
    }
}
