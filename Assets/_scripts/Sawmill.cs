using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sawmill", menuName = "Buildings/Sawmill", order = 1)]
public class Sawmill : BuildingScript
{
    public int ResourceQuantity;
    public float TimeInterval;
    private float resourceTimeStamp;

    public ResourceType Resource;

	// Use this for initialization
	void Start ()
    {
        GameManagerScript.Get.RegisterBuildings(this);
        resourceTimeStamp = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {	
        if( Time.time - resourceTimeStamp >= TimeInterval )
        {
            resourceTimeStamp = Time.time;
            PlayerScript.AddResource(Resource, ResourceQuantity);
        }
	}
    
    public override void OnUse()
    {
        throw new NotImplementedException();
    }
}
