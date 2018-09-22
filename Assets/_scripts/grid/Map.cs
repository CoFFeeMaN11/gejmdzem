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
    Tile [,] map;

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
        ret.x = Mathf.RoundToInt(_in.x -0.5f);
        ret.y = Mathf.RoundToInt(_in.y -0.5f);
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
        TileCoords currPos;
        for(int i = 1; i < road.WayPoints.Count; i++)
        {
            Waypoint wp1 = road.WayPoints[i - 1],
                wp2 = road.WayPoints[i];
            if(wp1.transform.position.x != wp2.transform.position.x)
            {
                currPos = FromVector(wp1.transform.position);
                do
                {
                    SerializedObject serialized = new SerializedObject(this[currPos]);
                    SerializedProperty property = serialized.FindProperty("type");
                    property.intValue = (int)TileType.ROAD;
                    serialized.ApplyModifiedProperties();
                    currPos.x -= Mathf.RoundToInt(Mathf.Sign(wp2.transform.position.x - wp1.transform.position.y));
                } while (currPos.x <= FromVector(wp2.transform.position).x);
            }
            else if (wp1.transform.position.y != wp2.transform.position.y)
            {
                currPos = FromVector(wp1.transform.position);
                do
                {
                    SerializedObject serialized = new SerializedObject(this[currPos]);
                    SerializedProperty property = serialized.FindProperty("type");
                    property.intValue = (int)TileType.ROAD;
                    serialized.ApplyModifiedProperties();
                    currPos.y += (int)Mathf.Sign(wp2.transform.position.y - wp1.transform.position.y);
                } while (currPos.y <= FromVector(wp2.transform.position).y);
            }
        }
    }

    public void CreateGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
            if(transform.GetChild(i) != null)
                DestroyImmediate(transform.GetChild(i).gameObject);
        map = new Tile[xSize, ySize];
        int index = 0;
        for (int i = 0; i < xSize; i++)
            for (int j = 0; j < ySize; j++)
            {
                var temp = new GameObject(string.Format("Tile{0:0000}",index++)).AddComponent<Tile>();
                temp.transform.parent = transform;
                temp.Create(i, j, tileSize);
                temp.gameObject.transform.position = transform.position + new Vector3((i+.5f)*tileSize, (j+.5f) * tileSize);
#if UNITY_EDITOR
                SerializedObject serializedObject = new SerializedObject(temp);
                SerializedProperty xProp = serializedObject.FindProperty("x"),
                    yProp = serializedObject.FindProperty("y"),
                    sizeProp = serializedObject.FindProperty("size");
                xProp.intValue = i;
                yProp.intValue = j;
                sizeProp.intValue = tileSize;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
#endif
                map[i,j] = temp;
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
