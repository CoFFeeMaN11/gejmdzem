using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class AttackSpeedUpgrade : TowerUpgrade, ITowerUpgrade
{
    public AttackSpeedUpgrade(ITowerUpgrade prev) : base(prev)
    {
    }

    public void Effect(TowerScript tower)
    {
        prevUpgrade.Effect(tower);
        tower.AttackSpeed *= 1.1f;
    }
}

