using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
[CreateAssetMenu(fileName = "Tower", menuName = "Buildings/Tower", order = 1)]
public class TowerScript : BuildingScript {
=======
public class TowerScript : MonoBehaviour {
>>>>>>> 286732f3e4b2a0373db72bf053be3c80cb95dd1f

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

    public UpgradeType[] upgrades;

    // Use this for initialization
    void Start ()
    {
        shootingTimeStamp = Time.time;
        upgrades = new UpgradeType[3];

        for(int i=0; i<upgrades.Length; i++)
        {
            Debug.Log(i);
            upgrades[i] = UpgradeType.empty;
        }
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
<<<<<<< HEAD
                //enemy.GetComponent<EnemyScript>().InflictDamage();
                var arrow = Instantiate(Arrow, GameManagerScript.Get.MainMap.ToVector(coords), Quaternion.identity) as GameObject;
=======
                var arrow = Instantiate(Arrow, transform.position, Quaternion.identity) as GameObject;
>>>>>>> 286732f3e4b2a0373db72bf053be3c80cb95dd1f
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

    void OnMouseDown()
    {
        BuildPanel.SetActive( !BuildPanel.activeInHierarchy );
    }
}
