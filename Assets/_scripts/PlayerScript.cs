using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public static int Gold;
    public static int Wood = 3000;
    public static int Stone;

    public Text WoodText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        WoodText.text = Wood.ToString();
	}
}
