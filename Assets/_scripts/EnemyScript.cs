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
    public float Damage;
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
	// Use this for initialization
	void Start ()
    {
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
                PlayerScript.AddResource(ResourceType.life, -1);
                Destroy(gameObject);
            }

            return;
        }
        
        Vector3 directionVector = new Vector3( GameManagerScript.Get.currentPath[pathNumber].Waypoints[waypointIterator].transform.position.x - transform.position.x + offset,
            GameManagerScript.Get.currentPath[pathNumber].Waypoints[waypointIterator].transform.position.y - transform.position.y, 0);


        transform.Translate(directionVector.normalized * MovementSpeed * Time.deltaTime);

        if( directionVector.x > 0.1f )
        {
            ren.sprite = spriteSide;
        }
        else if(directionVector.x < -.1f)
        {
            ren.sprite = defaultSprite;
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
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
            GameManagerScript.Get.enemyList.Remove(gameObject.transform);
            gameObject.SetActive(false);
        }
    }

}
