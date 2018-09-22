using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public static int Gold = 3000;
    public static int Wood = 3000;
    public static int Stone = 3000;

    public Text WoodText;
    public Text GoldText;
    public Text StoneText;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        WoodText.text = Wood.ToString();
        GoldText.text = Wood.ToString();
        StoneText.text = Wood.ToString();
    }
}
