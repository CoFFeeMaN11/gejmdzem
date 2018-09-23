using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    empty,
    EnemySpeed,
    Damage,
    AttackSpeed,
    Gold,
    ExplosiveArrows
}

public class UpgradeBuildingScript : MonoBehaviour
{

    UpgradeType Upgrade;

    public int BuildingTypeID;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

}
