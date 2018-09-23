using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceType
{
    gold,
    wood,
    stone,
    life
}

public class PlayerScript : MonoBehaviour
{

    public static int Gold = 3000;
    public static int Wood = 3000;
    public static int Stone = 3000;
    public static int Life = 500;

    public Text WoodText;
    public Text GoldText;
    public Text StoneText;
    public Text LifeText;

    // Use this for initialization
    void Start ()
    {
        WoodText.text = Wood.ToString();
        GoldText.text = Gold.ToString();
        StoneText.text = Stone.ToString();
        LifeText.text = "Life: " + Life.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        WoodText.text = Wood.ToString();
        GoldText.text = Gold.ToString();
        StoneText.text = Stone.ToString();
        LifeText.text = "Life: " + Life.ToString();
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
        else if( rt == ResourceType.life )
        {
            Life += q;

            if (Life <= 0)
                Time.timeScale = 0f;
        }
    }
}
