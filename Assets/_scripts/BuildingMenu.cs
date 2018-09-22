using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour {

    Animator animator;
    public BuildingItem item;
    public Transform context;
    TileCoords coords;
    List<BuildingItem> items = new List<BuildingItem>();
    
    public void Show(TileCoords _coords)
    {
        coords = _coords;
        foreach(var b in GameManagerScript.Get.GetAllBuildings())
        {
            var temp = Instantiate(item) as BuildingItem;
            temp.transform.parent = context.transform;
        }
        animator.SetBool("open", true);
    }

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
