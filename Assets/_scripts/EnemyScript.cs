using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    small,
    medium,
    big
}

public class EnemyScript : MonoBehaviour {

    public float MovementSpeed;
    public float Damage;
    public float Health;

    private Road road;

    private int waypointIterator = 0;
    private int roadNumber;
    private float offset;

    private bool stop = false;

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
        
        Vector3 directionVector = new Vector3(road.WayPoints[waypointIterator].position.x - transform.position.x + offset, road.WayPoints[waypointIterator].position.y - transform.position.y, transform.position.z);

        transform.Translate(directionVector.normalized * MovementSpeed * Time.deltaTime);

        if(Mathf.Abs(transform.position.y - road.WayPoints[waypointIterator].position.y) <= 0.1f)
        {
            if(waypointIterator < road.WayPoints.Length - 1)
            {
                waypointIterator++;
            }
            else
            {
                stop = true;
            }
        }
        
	}

    public void SetRoadAndOffset( Road r, float o )
    {
        road = r;
        offset = o;
    }

    public void InflictDamage( float damage )
    {
        Health -= damage;

        if (Health <= 0f)
        {
            PlayerScript.AddResource(ResourceType.gold, 10);
            GameManagerScript.Get.enemyList.Remove(gameObject.transform);
            gameObject.SetActive(false);
        }
    }

}
