using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TowerScript : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private int attackSpeed;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float shootingTimeInterval;
    private float shootingTimeStamp;
    
    private int attackCounter = 0;

    public GameObject Arrow;
    public GameObject FireArrow;
    //public GameObject BuildPanel;

    public List<UpgradeType> upgrades = new List<UpgradeType>();
    private int listCount = 0;

    private float attackSpeedVaryfier = 0f;
    private bool giveGold = false;
    private float goldInterval = 2f;
    private float goldTimeStamp;

    private bool explosive = false;

    private GameObject arrowObj;

    public Sprite CannonTowerSprite;

    // Use this for initialization
    void Start ()
    {
        shootingTimeStamp = Time.time;
        goldTimeStamp = Time.time;
        arrowObj = Arrow;
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
            CheckUpgrades();
        }

        if(giveGold && (Time.time - goldTimeStamp >= goldInterval))
        {
            PlayerScript.AddResource(ResourceType.gold, 5);
            goldTimeStamp = Time.time;
        }
	}

    void CheckUpgrades()
    {
        //cannon tower check
        int workspace = 0, explosiveArrows = 0;

        foreach( UpgradeType ups in upgrades )
        {
            if( ups == UpgradeType.Damage )
            {
                workspace++;
            }
            else if( ups == UpgradeType.ExplosiveArrows )
            {
                explosiveArrows++;
            }
        }

        if (workspace == 1 && explosiveArrows == 1)
        {
            GetComponent<SpriteRenderer>().sprite = CannonTowerSprite;
        }


    }

    void DealTheDeal( UpgradeType type )
    {
        switch(type)
        {
            case UpgradeType.ExplosiveArrows:
                //Debug.Log("toggling faja");
                arrowObj = FireArrow;
                explosive = true;
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
                var arrow = Instantiate(arrowObj, transform.position, Quaternion.identity) as GameObject;
                arrow.GetComponent<ArrowScript>().SetTarget(enemy);
                arrow.gameObject.GetComponent<ArrowScript>().Damage = damage;
                arrow.GetComponent<ArrowScript>().explodingArrow = explosive;
                attackCounter++;

                if (attackCounter == 1 + attackSpeedVaryfier)
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
