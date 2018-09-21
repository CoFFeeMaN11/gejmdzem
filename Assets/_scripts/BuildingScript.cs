using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingScript : ScriptableObject
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    protected int price;
    [SerializeField]
    [TextArea]
    protected string description;
    [SerializeField]
    private BuildingScript[] requiments;


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

    protected abstract void OnUse();
}
