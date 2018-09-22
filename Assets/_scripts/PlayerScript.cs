using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceType
{
    gold,
    wood,
    stone
}

public class PlayerScript : MonoBehaviour
{

    public static int Gold = 3000;
    public static int Wood = 3000;
    public static int Stone = 3000;

    public Text WoodText;
    public Text GoldText;
    public Text StoneText;

    // Use this for initialization
    void Start ()
    {
        WoodText.text = Wood.ToString();
        GoldText.text = Gold.ToString();
        StoneText.text = Stone.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        WoodText.text = Wood.ToString();
        GoldText.text = Gold.ToString();
        StoneText.text = Stone.ToString();

        if( Input.GetMouseButtonDown(0) )
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit, 100)) // or whatever range, if applicable
            {
                Debug.Log("out");
            }
        }
    }

    public static void AddResource( ResourceType rt, int q )
    {
        if( rt == ResourceType.gold )
        {
            Gold += q;
        }
        else if( rt == ResourceType.stone )
        {
            Stone += q;
        }
        else if( rt == ResourceType.wood )
        {
            Wood += q;
        }
    }
}
