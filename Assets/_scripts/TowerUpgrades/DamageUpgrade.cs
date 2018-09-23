using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade :  TowerUpgrade, ITowerUpgrade{
    public DamageUpgrade(ITowerUpgrade prev) : base(prev)
    {
    }

    public void Effect(TowerScript tower)
    {
        prevUpgrade.Effect(tower);
        tower.Damage = Mathf.RoundToInt(tower.Damage* 1.1f);
    }
}
