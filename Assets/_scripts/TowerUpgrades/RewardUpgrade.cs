using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RewardUpgrade : ITowerUpgrade
{
    ITowerUpgrade prevUpgrade;

    public RewardUpgrade(ITowerUpgrade prev)
    {
        prevUpgrade = prev;
    }
    public void Effect(TowerScript tower)
    {
        prevUpgrade.Effect(tower);
        tower.Rewand *= 1.1f;
    }
}
