using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class AttackSpeedUpgrade : ITowerUpgrade
{
    ITowerUpgrade prevUpgrade;

    public AttackSpeedUpgrade(ITowerUpgrade prev)
    {
        prevUpgrade = prev;
    }
    public void Effect(TowerScript tower)
    {
        prevUpgrade.Effect(tower);
        tower.AttackSpeed *= 1.1f;
    }
}

