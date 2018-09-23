using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RewardUpgrade : TowerUpgrade, ITowerUpgrade
{
    public RewardUpgrade(ITowerUpgrade prev) : base(prev)
    {
    }

    public void Effect(TowerScript tower)
    {
        prevUpgrade.Effect(tower);
        tower.Rewand *= 1.1f;
    }
}
