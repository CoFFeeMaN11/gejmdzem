using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour {

    private float verticalDirection = 1f;
    private float horizontalDirection = 0;

    public float MovementSpeed;
    public float Damage;

    private int waypointIterator = 0;

    private bool stop = false;

    public enum EnemyType
    {
        small,
        medium,
        big
    }

    public EnemyType Type;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (stop)
            return;
        
        Vector3 directionVector = new Vector3(GameManagerScript.Get.CurrentRoad.WayPoints[waypointIterator].position.x - transform.position.x, GameManagerScript.Get.CurrentRoad.WayPoints[waypointIterator].position.y - transform.position.y, transform.position.z);

        transform.Translate(directionVector.normalized * MovementSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, GameManagerScript.Get.CurrentRoad.WayPoints[waypointIterator].position) <= 0.1f)
        {
            if(waypointIterator < GameManagerScript.Get.CurrentRoad.WayPoints.Length - 1)
            {
                waypointIterator++;
            }
            else
            {
                stop = true;
            }
        }
        
	}

}
