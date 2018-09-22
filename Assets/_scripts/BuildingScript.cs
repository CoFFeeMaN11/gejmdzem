using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingScript : ScriptableObject
{
    [SerializeField]
    protected int id;
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
    protected BuildingScript[] requiments;
    [SerializeField]
    protected TileCoords coords;

    public int ID
    {
        get
        {
            return id;
        }
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
