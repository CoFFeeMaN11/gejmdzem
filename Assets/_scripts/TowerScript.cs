using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerScript : MonoBehaviour
{
    TileCoords coords;

    [SerializeField]
    private int damage;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float shootingTimeInterval;
    private float shootingTimeStamp;
    private float rewandRadio = 1f;
    private int attackCounter = 0;

    public GameObject Arrow;
    //public GameObject BuildPanel;


    public List<UpgradeType> upgrades = new List<UpgradeType>();
    private int listCount = 0;

    private float attackSpeedVaryfier = 0f;
    private bool giveGold = false;
    private float goldInterval = 2f;
    private float goldTimeStamp;

    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }


        set
        {
            if (value < 0) return;
            attackSpeed = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            if (value < 0) return;
            damage = value;
        }
    }

    public float Rewand
    {
        get
        {
            return rewandRadio;
        }

        set
        {
            rewandRadio = value;
        }
    }

    void Start ()
    {
        shootingTimeStamp = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if( Time.time - shootingTimeStamp >= shootingTimeInterval )
        {
            shootingTimeStamp = Time.time;
            attackCounter = 0;
            CheckRange();
        }

        if (upgrades.Count > listCount)
        {
            Debug.Log(upgrades[listCount]);
            DealTheDeal(upgrades[listCount]);
            listCount++;
        }

        if(giveGold && (Time.time - goldTimeStamp >= goldInterval))
        {
            PlayerScript.AddResource(ResourceType.gold, 5);
            goldTimeStamp = Time.time;
        }
	}

    void DealTheDeal( UpgradeType type )
    {
        switch(type)
        {
            case UpgradeType.ExplosiveArrows:
                //explosieve arrows
                break;

            case UpgradeType.AttackSpeed:
                attackSpeedVaryfier = 1f;
                break;

            case UpgradeType.Damage:
                damage += 10;
                break;

            case UpgradeType.Gold:
                giveGold = true;
                break;
        }
    }

    void CheckRange()
    {
        foreach( Transform enemy in GameManagerScript.Get.enemyList )
        {
            if(enemy == null)
            {
                continue;
            }

            if (Vector3.Distance(enemy.position, transform.position) <= attackRange)
            {
                var arrow = Instantiate(Arrow, transform.position, Quaternion.identity) as GameObject;
                arrow.GetComponent<ArrowScript>().SetTarget(enemy);
                arrow.gameObject.GetComponent<ArrowScript>().Damage = Damage;
                attackCounter++;

                if (attackCounter == 2 + attackSpeedVaryfier)
                {
                    break;
                }
            }
        }
    }


    void OnMouseDown()
    {
        if(GameManagerScript.ScreenFree)
        {
            GameManagerScript.Get.UpgradePanel.SetActive(true);
            PosStamp.obj = gameObject;
        }
    }
}
