using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour {

    [SerializeField]
    private int tileSize = 3;
    [SerializeField]
    private int xSize = 10;
    [SerializeField]
    private int ySize = 10;
    private List<Tile> map = new List<Tile>();

    public Tile this[int x, int y]
    {
        get
        {
            if (x < xSize && y < ySize && x >= 0 && y >= 0)
                return map[x + y * xSize];
            else throw new System.IndexOutOfRangeException();
        }
    }
    public void CreateGrid()
    {
        map.Clear();
        for (int i = 0; i < xSize; i++)
            for (int j = 0; j < ySize; j++)
            {
                Tile temp = ScriptableObject.CreateInstance<Tile>();
                temp.Create(i, j, tileSize);
                map.Add(temp);
            }
        Debug.Log("Create grid");
    }

    private void OnDrawGizmos()
    {
        if (map == null) return;
        for (int i = 0; i < xSize; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position + new Vector3(i * tileSize, 0), transform.position + new Vector3(i * tileSize, xSize * tileSize));
            for (int j = 0; j < ySize; j++)
            {
                Gizmos.color = Tile.tileColors[(int)this[i, j].Type];
                Gizmos.DrawCube(transform.position + new Vector3((i + 0.5f) * tileSize, (j + 0.5f) * tileSize), new Vector3(tileSize, tileSize));
                Gizmos.color = Color.black;
                Gizmos.DrawLine(transform.position + new Vector3(0, j*tileSize), transform.position + new Vector3(tileSize * ySize, j * tileSize));
            }
        }
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position + new Vector3(xSize * tileSize, 0), transform.position + new Vector3(xSize * tileSize, xSize * tileSize));
        Gizmos.DrawLine(transform.position + new Vector3(0, ySize * tileSize), transform.position + new Vector3(tileSize * ySize, ySize * tileSize));
    }


    // Use this for initialization
    void Start () {
        CreateGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
