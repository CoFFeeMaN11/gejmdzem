using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTowerPrefeb : MonoBehaviour {

    ITowerUpgrade upgradesObj;
    [SerializeField]
    [EnumFlags]
    TowerUpgradeType upgrades;
    public TowerScript tower;
    public ITowerUpgrade Upgrades
    {
        get
        {
            return upgradesObj;
        }
    }

    // Use this for initialization
    void Start () {
        upgradesObj = new EmptyTowerUpgrade();
        TowerUpgradeType upgradesCopy = upgrades;
        ITowerUpgrade buffer;
        while (upgradesCopy != 0)
        {
            buffer = TowerUpgrade.AddUpgrade(upgradesObj, ref upgradesCopy);
            upgradesObj = buffer;
        }
        if (tower != null)
            upgradesObj.Effect(tower);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
