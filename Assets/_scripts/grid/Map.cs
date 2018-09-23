using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Map : MonoBehaviour {

    [SerializeField]
    private int tileSize = 3;
    [SerializeField]
    private int xSize = 10;
    [SerializeField]
    private int ySize = 10;
    [SerializeField]
    Tile[,] map;

    public Tile this[int x, int y]
    {
        get
        {
            if (x < xSize && y < ySize && x >= 0 && y >= 0)
                return map[x,y];
            else throw new System.IndexOutOfRangeException();
        }
    }
    public Tile this[TileCoords _in]
    {
        get
        {
            if (_in.x < xSize && _in.y < ySize && _in.x >= 0 && _in.y >= 0)
                return map[_in.x,_in.y];
            else throw new System.IndexOutOfRangeException();
        }
    }
    public bool Exists (int x, int y)
    {
        return (x < xSize && y < ySize && x >= 0 && y >= 0);
    }

    public TileCoords FromVector(Vector3 _in)
    {
        TileCoords ret;
        _in -= transform.position;
        _in.x /= tileSize;
        _in.y /= tileSize;
        ret.x = Mathf.RoundToInt(_in.x);
        ret.y = Mathf.RoundToInt(_in.y);
        return ret;
    }
    public Vector3 ToVector(TileCoords _in)
    {
        Vector3 ret = new Vector3();
        ret.x = (_in.x+.5f) * tileSize;
        ret.y = (_in.y+.5f) * tileSize;
        ret += transform.position;
        return ret;
    }
    public void ResetMap()
    {
        foreach (var t in map)
            t.Type = TileType.STANDARD;
    }

    public void BakeRoad(Road road, RoadSpritePack pack)
    {
        GameObject spriteGroup = new GameObject("RoadGraphics");
        spriteGroup.transform.parent = road.transform;
        spriteGroup.transform.position = road.WayPoints[0].transform.position;
        GameObject temp;
        temp = new GameObject("road");
        temp.transform.parent = spriteGroup.transform;
        temp.AddComponent<SpriteRenderer>().sortingOrder = -1;
        temp.transform.position = road.WayPoints[0].transform.position;
        SerializedObject serialized = new SerializedObject(this[FromVector(road.WayPoints[0].transform.position)]);
        SerializedProperty property = serialized.FindProperty("type");
        property.intValue = (int)TileType.ROAD;
        serialized.ApplyModifiedProperties();
        if (road.WayPoints == null || road.WayPoints.Count < 2) return;
        TileCoords wp1, wp2;
        TileCoords diff;
        for (int i = 0; i < road.WayPoints.Count; i++)
        {
            if(i == 0)
            {
                wp1 = FromVector(road.WayPoints[0].transform.position);
                wp2 = FromVector(road.WayPoints[1].transform.position);
                diff = wp2 - wp1;
                if (diff.x == 0)
                    temp.GetComponent<SpriteRenderer>().sprite = pack.vertical;
                else
                    temp.GetComponent<SpriteRenderer>().sprite = pack.horizontal;
                continue;
            }
            wp1 = FromVector(road.WayPoints[i - 1].transform.position);
            wp2 = FromVector(road.WayPoints[i].transform.position);
            diff = wp2 - wp1;
            int steps = Mathf.Abs(diff.x) > Mathf.Abs(diff.y) ? Mathf.Abs(diff.x) : Mathf.Abs(diff.y);
            Debug.Log(steps);
            TileCoords step = new TileCoords(diff.x / steps, diff.y / steps);
            Vector2 currPoint = ToVector(wp1);
            TileCoords coords = wp1;
            temp = new GameObject("road");
            temp.transform.parent = spriteGroup.transform;
            temp.AddComponent<SpriteRenderer>().sortingOrder = -1;
            temp.transform.position = ToVector(wp2);
            serialized = new SerializedObject(this[wp2]);
            property = serialized.FindProperty("type");
            property.intValue = (int)TileType.ROAD;
            serialized.ApplyModifiedProperties();
            //temp.GetComponent<SpriteRenderer>().sortingLayerID = -1;
            if (i + 1 == road.WayPoints.Count )
            {
                if (diff.x == 0)
                    temp.GetComponent<SpriteRenderer>().sprite = pack.vertical;
                else
                    temp.GetComponent<SpriteRenderer>().sprite = pack.horizontal;
            }
            else if (i != 0)
            {
                temp.transform.position = ToVector(wp2);
                TileCoords wp3 = FromVector(road.WayPoints[i + 1].transform.position);
                TileCoords diff21 = wp2 - wp1, diff32 = wp3 - wp2;
                if (diff21.y > 0)
                    temp.GetComponent<SpriteRenderer>().sprite = diff32.x > 0 ? pack.DownRight : pack.DownLeft;
                else if (diff21.y < 0)
                    temp.GetComponent<SpriteRenderer>().sprite = diff32.x > 0 ? pack.UpRight : pack.UpLeft;
                else if (diff21.x > 0)
                    temp.GetComponent<SpriteRenderer>().sprite = diff32.y > 0 ? pack.UpLeft : pack.DownLeft;
                else if (diff21.x < 0)
                    temp.GetComponent<SpriteRenderer>().sprite = diff32.y < 0 ? pack.DownRight : pack.UpRight;

            }
            
            for (int v=1; v < steps; v++)
            {
                serialized = new SerializedObject(this[coords + v*step]);
                property = serialized.FindProperty("type");
                property.intValue = (int)TileType.ROAD;
                serialized.ApplyModifiedProperties();
                temp = new GameObject("road");
                temp.transform.parent = spriteGroup.transform;
                temp.AddComponent<SpriteRenderer>().sortingOrder = -1;
                temp.transform.position = ToVector(coords + v * step);

                if (diff.x == 0)
                    temp.GetComponent<SpriteRenderer>().sprite = pack.vertical;
                else
                    temp.GetComponent<SpriteRenderer>().sprite = pack.horizontal;
            }
            temp = new GameObject("road");
            temp.transform.parent = spriteGroup.transform;
            temp.AddComponent<SpriteRenderer>().sortingOrder = -1;
            if (i + 1 == road.WayPoints.Count)
            {
                temp.transform.position = ToVector(coords + steps * step);
                if (diff.x == 0)
                    temp.GetComponent<SpriteRenderer>().sprite = pack.vertical;
                else
                    temp.GetComponent<SpriteRenderer>().sprite = pack.horizontal;
            }
            
        }
    }
    public void Recovery()
    {
        map = new Tile[xSize, ySize];
        for (int i = 0; i < transform.childCount; i++)
        {
            var temp = transform.GetChild(i).GetComponent<Tile>();
            map[temp.Coords.x, temp.Coords.y] = temp;
        }


    }
    public void CreateGrid()
    {
        if(map != null)
            foreach (var t in map)
                if (t != null)
                    DestroyImmediate(t.gameObject);
        map = new Tile[xSize, ySize];  
        int index = 0;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                var temp = new GameObject(string.Format("Tile{0:0000}", index++)).AddComponent<Tile>();
                temp.transform.parent = transform;
                temp.Create(i, j, tileSize);
                temp.gameObject.transform.position = transform.position + new Vector3((i + .5f) * tileSize, (j + .5f) * tileSize);
#if UNITY_EDITOR
                SerializedObject serializedObject = new SerializedObject(temp);
                SerializedProperty xProp = serializedObject.FindProperty("coords.x"),
                    yProp = serializedObject.FindProperty("coords.y"),
                    sizeProp = serializedObject.FindProperty("size");
                xProp.intValue = i;
                yProp.intValue = j;
                sizeProp.intValue = tileSize;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
#endif
                map[i,j] = temp;
            }
        }
        Debug.Log("Create grid");
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < xSize; i++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(i * tileSize, 0), transform.position + new Vector3(i * tileSize, ySize * tileSize));
            for (int j = 0; j < ySize; j++)
                Gizmos.DrawLine(transform.position + new Vector3(0, j*tileSize), transform.position + new Vector3(tileSize * xSize, j * tileSize));
        }
        Gizmos.DrawLine(transform.position + new Vector3(xSize * tileSize, 0), transform.position + new Vector3(xSize * tileSize, ySize * tileSize));
        Gizmos.DrawLine(transform.position + new Vector3(0, ySize * tileSize), transform.position + new Vector3(tileSize * xSize, ySize * tileSize));
    }




    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
