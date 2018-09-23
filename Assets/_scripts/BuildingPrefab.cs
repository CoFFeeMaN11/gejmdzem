using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPrefab : MonoBehaviour {

    [SerializeField]
    private int maxHP = 100;
    private int hp;
    [SerializeField]
    private Building building;

    public int id
    {
        get
        {
            return building.Id;
        }
    }
	// Use this for initialization
	void Start () {
        Repair();
	}
	public void Repair()
    {
        hp = maxHP;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
