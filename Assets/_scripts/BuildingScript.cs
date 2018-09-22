using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum TerrainType
{
    GRASS,
    ROAD,
    FOREST,
    ROCKS,
}

public abstract class BuildingScript : ScriptableObject, System.IComparable<BuildingScript>
{
    [SerializeField]
    protected int maxHP;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected Sprite sprite;
    [SerializeField]
    private int stone;
    [SerializeField]
    [TextArea]
    protected string description;
    [SerializeField]
    protected BuildingScript[] requiments;
    [SerializeField]
    protected TileCoords coords;
    [SerializeField]
    protected TerrainType canBuildOn;
    [SerializeField]
    private int gold;
    [SerializeField]
    private int wood;

    protected int Gold
    {
        get
        {
            return gold;
        }
    }

    protected int Wood
    {
        get
        {
            return wood;
        }
    }

    protected int Stone
    {
        get
        {
            return stone;
        }
    }



    // Update is called once per frame
    void Update ()
    {
		
	}

    void Repair()
    {
        health = maxHP;
    }

    void GetUpgrades()
    {

    }

    public abstract void OnUse();

    public int CompareTo(BuildingScript other)
    {
        return GameManagerScript.Hash(this.name) - GameManagerScript.Hash(other.name);
    }
}
