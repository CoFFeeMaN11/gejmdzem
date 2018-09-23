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

    private int pathNumber;

    public Sprite spriteSide;
    private Sprite defaultSprite;
    private SpriteRenderer ren;

    public float MovementSpeed;
    public int Damage;
    public float Health;

    public Queue<Transform> road = new Queue<Transform>();

    private int waypointIterator = 0;
    private int roadNumber;
    private float offset;
    public float dist;
    private bool stop = false;

    private float dissapearTime = 1f;
    private float dissapearTimeStamp;

    public EnemyType Type;

    private float explosiveRadius = 3f;

    public AudioSource audioSrc;
	// Use this for initialization
	void Start ()
    {
        audioSrc = GetComponent<AudioSource>();
        stop = false;
        ren = GetComponent<SpriteRenderer>();
        defaultSprite = ren.sprite;
    }
	
	// Update is called once per frame
	void Update ()
    {
        MovementSpeed *= GameManagerScript.EnemySpeedVaryfier;

        if (stop)
        {
            //attacking animation
            if( Time.time - dissapearTimeStamp >= dissapearTime )
            {
                audioSrc.Play();
                PlayerScript.AddResource(ResourceType.life, -Damage);
                Destroy(gameObject);
            }

            return;
        }
        
        Vector3 directionVector = new Vector3( GameManagerScript.Get.currentPath[pathNumber].Waypoints[waypointIterator].transform.position.x - transform.position.x + offset,
            GameManagerScript.Get.currentPath[pathNumber].Waypoints[waypointIterator].transform.position.y - transform.position.y, 0);


        transform.Translate(directionVector.normalized * MovementSpeed * Time.deltaTime);

        if( Mathf.Abs(directionVector.x) - Mathf.Abs(directionVector.y) >= 0 )
        {
            if(directionVector.x > 0)
            {
                ren.sprite = spriteSide;

                if(transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                }

            }
            else
            {
                ren.sprite = spriteSide;

                if( transform.localScale.x > 0 )
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
                }
            }
        }
        else
        {
            ren.sprite = defaultSprite;
        }
        
        dist = Vector2.Distance(transform.position, GameManagerScript.Get.currentPath[pathNumber].Waypoints[waypointIterator].transform.position);
        if (dist <= 1f)
        {
            if (waypointIterator < GameManagerScript.Get.currentPath[pathNumber].Waypoints.Length - 1)
            {
                waypointIterator++;
            }
            else
            {
                stop = true;
                dissapearTimeStamp = Time.time;
                //Destroy(gameObject);
                //return;
            }
        }
        
    }

    public void SetRoadAndOffset( int path, float o )
    {
        pathNumber = path;
        offset = o;
    }

    public void InflictDamage( float damage )
    {
        Health -= damage;

        if (Health <= 0f)
        {
            PlayerScript.AddResource(ResourceType.gold, 10);
            //GameManagerScript.Get.enemyList.Remove(gameObject.transform);
            Destroy(gameObject);
        }
    }

    public void InflictDamageWithExplosive(float damage)
    {
        Health -= damage;

        foreach(Transform enemies in GameManagerScript.Get.enemyList)
        {
            if (enemies == null && enemies != this.transform)
                continue;

            if( Vector3.Distance(transform.position, enemies.transform.position) <= explosiveRadius )
            {
                enemies.gameObject.GetComponent<EnemyScript>().InflictDamage(damage);
            }
        }

        if (Health <= 0f)
        {
            PlayerScript.AddResource(ResourceType.gold, 10);
            //GameManagerScript.Get.enemyList.Remove(gameObject.transform);
            Destroy(gameObject);
        }
    }

}
