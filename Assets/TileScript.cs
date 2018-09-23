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

public enum TileType
{
    tower,
    stone,
    wood
}

public class TileScript : MonoBehaviour {

    private Button button;
    private bool enabled = true;

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

    void OnMouseDown()
    {
        if( enabled  && GameManagerScript.ScreenFree)
        {
            GameManagerScript.Get.ClearScreen();

            switch(type)
            {
                case TileType.stone:
                    QuarryPanel.SetActive(true);
                    break;

                case TileType.tower:
                    TowerBuildPanel.SetActive(true);

                    QuarryPanel.SetActive(false);
                    break;

                case TileType.wood:
                    SawmillPanel.SetActive(true);

                    QuarryPanel.SetActive(false);
                    break;
            }

            PosStamp.pos = transform.position;
            PosStamp.offset = Vector3.up * offset;
            PosStamp.obj = gameObject;
        }
    }

    public void Disable()
    {
        enabled = false;
    }
}
