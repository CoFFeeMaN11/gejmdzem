using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class TowerScript : MonoBehaviour
{
    TileCoords coords;
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
    public GameObject BuildPanel;


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
                arrow.gameObject.GetComponent<ArrowScript>().Damage = damage;
                attackCounter++;

                if (attackCounter == 2)
                {
                    break;
                }
            }
        }
    }

}
