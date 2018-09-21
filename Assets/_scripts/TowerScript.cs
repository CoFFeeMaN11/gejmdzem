using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour {

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

    // Use this for initialization
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
            if( Vector3.Distance(enemy.position, transform.position ) <= attackRange )
            {
                enemy.GetComponent<EnemyScript>().InflictDamage();
                attackCounter++;

                if (attackCounter == 2)
                    break;
            }
        }
    }
}
