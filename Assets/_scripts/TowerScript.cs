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
    public GameObject BuildPanel;

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
	}

    void CheckRange()
    {
        foreach( Transform enemy in GameManagerScript.Get.enemyList )
        {
            if (TileCoords.Distance(GameManagerScript.Get.MainMap.FromVector(enemy.position), coords) <= attackRange)
            {
                var arrow = Instantiate(Arrow, GameManagerScript.Get.MainMap.ToVector(coords), Quaternion.identity) as GameObject;
                arrow.GetComponent<ArrowScript>().SetTarget(enemy);
                arrow.gameObject.GetComponent<ArrowScript>().Damage = Damage;
                attackCounter++;

                if (attackCounter == 2)
                {
                    break;
                }
            }
        }
    }

}
