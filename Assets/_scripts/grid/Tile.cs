using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TileCoords
{
    public int x, y;
    public TileCoords(int p1, int p2)
    {
        x = p1;
        y = p2;
    }
    public static TileCoords operator +(TileCoords a, TileCoords b)
    {
        TileCoords ret;
        ret.x = a.x + b.x;
        ret.y = a.y + b.y;
        return ret;
    }
    public static TileCoords operator *(int a, TileCoords b)
    {
        TileCoords ret;
        ret.x = a* b.x;
        ret.y = a*b.y;
        return ret;
    }
    public static TileCoords operator -(TileCoords a, TileCoords b)
    {
        TileCoords ret;
        ret.x = a.x - b.x;
        ret.y = a.y - b.y;
        return ret;
    }
    private static TileCoords[] Neighbors =
    {
        new TileCoords(1,0), new TileCoords(1,1), new TileCoords(0,1),
        new TileCoords(-1,1), new TileCoords(-1,0), new TileCoords(-1,-1),
        new TileCoords(0,-1), new TileCoords(1,-1)
    };
    public TileCoords GetNeighbor(int i)
    {
        return this + Neighbors[i % 8];
    }
    public override string ToString()
    {
        return string.Format("({0},{1})", x, y);
    }
    public static int Distance(TileCoords a, TileCoords b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
}

public enum TileType
{
    STANDARD,
    ROAD,
    FOREST,
    ROCKS,
    SELECTED,
    BUILDING,
}
[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class Tile : MonoBehaviour {
    [SerializeField]
    private TileCoords coords;
    [SerializeField]
    private int size;
    [SerializeField]
    private TileType type = TileType.STANDARD;
    private GameObject building;

    public AudioSource audioSrc;

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

    public GameObject Building
    {
        get
        {
            return building;
        }
    }

    public TileCoords Coords
    {
        get
        {
            return coords;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = tileColors[(int)type];
        Gizmos.DrawCube(transform.position, new Vector3(size, size));
    }
    public void Create(int _x, int _y, int size, TileType _type = TileType.STANDARD)
    {
        coords.x = _x;
        coords.y = _y;
        if (_type == TileType.BUILDING)
            _type = TileType.STANDARD;
    }

    public bool Build(GameObject _building)
    {
        if (type == TileType.BUILDING) return false;
        type = TileType.BUILDING;
        building = Instantiate(_building,transform);
        audioSrc.Play();
        return true;
    }

    public void OnUse()
    {
        if (type != TileType.BUILDING)
            GameManagerScript.Get.OpenBuildMenu(coords);
        else
            building.SendMessage("OnUse");
    }
}
