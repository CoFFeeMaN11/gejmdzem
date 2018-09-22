using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : MonoBehaviour
{

    public int ResourceQuantity;
    public float TimeInterval;
    private float resourceTimeStamp;

	// Use this for initialization
	void Start () {
        resourceTimeStamp = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
        if( Time.time - resourceTimeStamp >= TimeInterval )
        {
            resourceTimeStamp = Time.time;
            PlayerScript.Wood += ResourceQuantity;
        }
	}
}
