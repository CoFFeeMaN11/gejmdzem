using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct PosStamp
{
    public static Vector3 pos;
    public static Vector3 offset;
    public static GameObject obj;
}



public class TileScript : MonoBehaviour {

    private Button button;

    public TileType type;

    public GameObject TowerBuildPanel;
    public GameObject SawmillPanel;
    public GameObject QuarryPanel;

    public float offset;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}


    public void Disable()
    {
        enabled = false;
    }
}
