using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", order = 1)]

public class BuildingScript : ScriptableObject
{
    [SerializeField]
    private int health;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int price;
    [SerializeField]
    [TextArea]
    private string description;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Repair()
    {

    }

    void GetUpgrades()
    {

    }

    void Used()
    {

    }
}
