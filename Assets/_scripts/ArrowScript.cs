using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    private Transform target;
    private Vector3 directionVector;

    public float MoveSpeed;
    public float LifeTime;

    private float startTime;

    public float Damage;

	// Use this for initialization
	void Start ()
    {
        startTime = Time.time;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate((directionVector - transform.position).normalized * MoveSpeed * Time.deltaTime);

        if ( target != null && Vector3.Distance(target.position, transform.position) <= 0.5f)
        {
            target.gameObject.GetComponent<EnemyScript>().InflictDamage( Damage );
            //target.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        if( Vector3.Distance(directionVector, transform.position) <= 0.1f )
        {
            gameObject.SetActive(false);
        }
	}

    public void SetTarget( Transform t )
    {
        target = t;
        directionVector = t.position;
    }
}
