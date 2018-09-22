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

    public Queue<Transform> road = new Queue<Transform>();

    private int waypointIterator = 0;
    private int roadNumber;
    private float offset;
    public float dist;
    private bool stop = false;

    public EnemyType Type;
	// Use this for initialization
	void Start ()
    {
        stop = false;
        waypointIterator = 1;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (stop || road.Count == 0)
            return;


        
        Vector3 directionVector = new Vector3(road.Peek().position.x - transform.position.x + offset,
            road.Peek().position.y - transform.position.y);

        transform.Translate(directionVector.normalized * MovementSpeed * Time.deltaTime);
        dist = Vector2.Distance(transform.position, road.Peek().position);
        if (dist <= 1f)
        {
            if (road.Count != 0)
            {
                waypointIterator++;
                road.Dequeue();
            }
            else
            {
                stop = true;
                Destroy(gameObject);
                return;
            }
        }
    }

    public void SetRoadAndOffset( Road r, float o )
    {
        foreach (var i in r.WayPoints)
            road.Enqueue(i.transform);
        offset = o;
        road.Dequeue();

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
