using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
=======

>>>>>>> A1ml0ck
public class Sawmill : MonoBehaviour
{
    public int ResourceQuantity;
    public float TimeInterval;
    private float resourceTimeStamp;

    public ResourceType Resource;

	// Use this for initialization
	void Start ()
    {
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
<<<<<<< HEAD
   
=======
    
    public void OnUse()
    {
        throw new NotImplementedException();
    }
>>>>>>> A1ml0ck
}
