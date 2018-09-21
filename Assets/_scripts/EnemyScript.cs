using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private float verticalDirection = 1f;
    private float horizontalDirection = 0;

    public float MovementSpeed;

    public Transform[] WayPoints;
    private int waypointIterator = 0;

    private bool stop = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (stop)
            return;

        Vector3 directionVector = new Vector3(WayPoints[waypointIterator].position.x - transform.position.x, WayPoints[waypointIterator].position.y - transform.position.y, transform.position.z);

        transform.Translate(directionVector.normalized * MovementSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, WayPoints[waypointIterator].position) <= 0.1f)
        {
            if(waypointIterator < WayPoints.Length - 1)
            {
                waypointIterator++;
            }
            else
            {
                stop = true;
            }
        }
	}

    void Turn( EnemyDirection direction )
    {

    }
}
