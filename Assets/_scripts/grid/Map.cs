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
    List<Tile> map = new List<Tile>();

    public Tile this[int x, int y]
    {
        get
        {
            if (x < xSize && y < ySize && x >= 0 && y >= 0)
                return map[xSize* x + y];
            else throw new System.IndexOutOfRangeException();
        }
    }
    public bool Exists (int x, int y)
    {
        return (x < xSize && y < ySize && x >= 0 && y >= 0);
    }
    public int[] FromVector(Vector3 _in)
    {
        int[] ret = new int[2];
        _in -= transform.position;
        _in.x /= tileSize;
        _in.y /= tileSize;
        ret[0] = Mathf.RoundToInt(_in.x -0.5f);
        ret[1] = Mathf.RoundToInt(_in.y -0.5f);
        return ret;
    }
    public void ResetMap()
    {
        foreach (var t in map)
            t.Type = TileType.STANDARD;
    }

    public void CreateGrid()
    {
        foreach (var t in map)
            if(t != null)
            DestroyImmediate(t.gameObject);
        map.Clear();
        for (int i = 0; i < xSize; i++)
            for (int j = 0; j < ySize; j++)
            {
                var temp = new GameObject(string.Format("Tile{0:0000}",map.Count)).AddComponent<Tile>();
                temp.transform.parent = transform;
                temp.Create(i, j, tileSize);
                temp.gameObject.transform.position = transform.position + new Vector3(i*tileSize, j * tileSize);
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
                map.Add(temp);
            }
        Debug.Log("Create grid");
    }

    private void OnDrawGizmos()
    {
        if (map == null) return;
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
