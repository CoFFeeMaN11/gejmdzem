using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    private Transform target;
    private Vector3 directionVector;

    public float MoveSpeed;
    public float LifeTime;

    private float startTime;

	// Use this for initialization
	void Start ()
    {
        startTime = Time.time;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate((directionVector - transform.position).normalized * MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(target.position, transform.position) <= 0.5f)
        {
            target.gameObject.GetComponent<EnemyScript>().InflictDamage();
            //target.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        if( Vector3.Distance(directionVector, transform.position) <= 0.1f )
        {
            gameObject.SetActive(false);
        }

        return;
        if ( Time.time - startTime >= LifeTime )
        {
            if (Vector3.Distance(target.position, transform.position) <= 0.3f)
            {
                //target.gameObject.SetActive(false);
                GameManagerScript.Get.enemyList.Remove(target);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("miss");
                gameObject.SetActive(false);
            }
        }
	}

    public void SetTarget( Transform t )
    {
        target = t;
        directionVector = t.position;
    }
}
