using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade :  ITowerUpgrade{
    
    ITowerUpgrade prevUpgrade;

    public DamageUpgrade(ITowerUpgrade prev)
    {
        prevUpgrade = prev;
    }
    public void Effect(TowerScript tower)
    {
        prevUpgrade.Effect(tower);
        tower.Damage = Mathf.RoundToInt(tower.Damage* 1.1f);
    }
}
