using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ThingToBuild
{
    tower,
    sawmill,
    quarry,
    alchemist,
    shootingRange,
    workshop,
    thievesGuild
}

public class ButtonScript : MonoBehaviour
{
    private Button button;

    private AudioSource audioSrc;

    public bool UpgradeButton;

    public ThingToBuild ToBuild;

    public GameObject ItsPanel;

    // Use this for initialization
    void Start () {
        audioSrc = GameObject.Find("Canvas").GetComponent<AudioSource>();
        button = GetComponent<Button>();
        if(button != null)
        {
            Debug.Log("awndinainwnadna");
        }
        button.onClick.AddListener(Build);

        if( UpgradeButton )
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(BuildUp);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Build()
    {
        GameObject building = null;
        if(ToBuild == ThingToBuild.tower)
        {
            //build tower
            building = GameManagerScript.Get.buildings.Tower;
        }
        else if( ToBuild == ThingToBuild.quarry )
        {
            building = GameManagerScript.Get.buildings.Quarry;
        }
        else if( ToBuild == ThingToBuild.sawmill )
        {
            building = GameManagerScript.Get.buildings.Sawmill;
        }

        if(CanYouAffordIt(building))
        {
            PosStamp.obj.GetComponent<TileScript>().Disable();
            audioSrc.Play();
            Instantiate(building, PosStamp.pos + PosStamp.offset, Quaternion.identity, GameObject.Find("Tilemap").transform);
            PlayerScript.AddResource(ResourceType.gold, -building.GetComponent<BuildingScript>().Price[0]);
            PlayerScript.AddResource(ResourceType.wood, -building.GetComponent<BuildingScript>().Price[1]);
            PlayerScript.AddResource(ResourceType.stone, -building.GetComponent<BuildingScript>().Price[2]);
        }
        ItsPanel.SetActive(false);
    }

    public void BuildUp()
    {
        GameObject building = null;
        if (ToBuild == ThingToBuild.alchemist)
        {
            building = GameManagerScript.Get.buildings.Alchemist;
        }
        else if (ToBuild == ThingToBuild.shootingRange)
        {
            building = GameManagerScript.Get.buildings.ShootingRange;
        }
        else if (ToBuild == ThingToBuild.workshop)
        {
            building = GameManagerScript.Get.buildings.Workshop;
        }
        else if (ToBuild == ThingToBuild.thievesGuild)
        {
            building = GameManagerScript.Get.buildings.ThievesGuild;
        }

        if (CanYouAffordIt(building))
        {
            StartCoroutine( WaitForClick(building) );
        }
        //ItsPanel.SetActive(false);
    }

    bool CanYouAffordIt( GameObject obj )
    {
        if (PlayerScript.Gold < obj.GetComponent<BuildingScript>().Price[0])
            return false;

        if (PlayerScript.Wood < obj.GetComponent<BuildingScript>().Price[1])
            return false;

        if (PlayerScript.Stone < obj.GetComponent<BuildingScript>().Price[2])
            return false;

        return true;
    }

    public IEnumerator WaitForClick( GameObject building )
    {
        GameManagerScript.ScreenFree = false;
        yield return new WaitForSecondsRealtime(0.1f);
        while (true)
        {
            if (Input.GetMouseButton(0) )
            {
                
                Debug.Log(Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), PosStamp.obj.transform.position));
                if (Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), PosStamp.obj.transform.position) < 15.0f)
                {
                    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    audioSrc.Play();
                    Instantiate(building, new Vector3(pos.x, pos.y, 0), Quaternion.identity, GameObject.Find("Tilemap").transform);
                    PlayerScript.AddResource(ResourceType.gold, -building.GetComponent<BuildingScript>().Price[0]);
                    PlayerScript.AddResource(ResourceType.wood, -building.GetComponent<BuildingScript>().Price[1]);
                    PlayerScript.AddResource(ResourceType.stone, -building.GetComponent<BuildingScript>().Price[2]);
                    PosStamp.obj.GetComponent<TowerScript>().upgrades.Add(building.GetComponent<BuildingScript>().upgrade);
                }
                GameManagerScript.ScreenFree = true;
                ItsPanel.SetActive(false);
                break;
            }

            yield return null;
        }
    }
}
