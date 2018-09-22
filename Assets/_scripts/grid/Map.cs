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

    public void BakeRoad(Road road)
    {
        if (road.WayPoints == null || road.WayPoints.Count < 2) return;
        for(int i = 1; i < road.WayPoints.Count; i++)
        {
            TileCoords wp1 = FromVector(road.WayPoints[i - 1].transform.position),
                wp2 = FromVector(road.WayPoints[i].transform.position);
            TileCoords diff = wp2 - wp1;
            int steps = Mathf.Abs(diff.x) > Mathf.Abs(diff.y) ? Mathf.Abs(diff.x) : Mathf.Abs(diff.y);
            Debug.Log(steps);
            TileCoords step = new TileCoords(diff.x / steps, diff.y / steps);
            Vector2 currPoint = ToVector(wp1);
            TileCoords coords = wp1;
            for(int v=0; v <= steps; v++)
            {
                
                SerializedObject serialized = new SerializedObject(this[coords]);
                SerializedProperty property = serialized.FindProperty("type");
                property.intValue = (int)TileType.ROAD;
                serialized.ApplyModifiedProperties();
                coords = coords + step;
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
