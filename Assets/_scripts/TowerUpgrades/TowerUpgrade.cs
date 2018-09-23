using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Flags]
public enum TowerUpgradeType
{

    DAMAGE=(1<<0),
    ATTACK_SPEED=(1<<1),
    REWARD=(1<<2),

}
public abstract class TowerUpgrade
{
    protected ITowerUpgrade prevUpgrade;
    public TowerUpgrade(ITowerUpgrade prev)
    {
        prevUpgrade = prev;
    }
    public static ITowerUpgrade AddUpgrade(ITowerUpgrade prev,ref TowerUpgradeType type)
    {
        if((type & TowerUpgradeType.DAMAGE) != 0)
        {
            type &= ~TowerUpgradeType.DAMAGE;
            return new DamageUpgrade(prev);
        }
        if ((type & TowerUpgradeType.ATTACK_SPEED) != 0)
        {
            type &= ~TowerUpgradeType.ATTACK_SPEED;
            return new AttackSpeedUpgrade(prev);
        }
        if ((type & TowerUpgradeType.REWARD) != 0)
        {
            type &= ~TowerUpgradeType.REWARD;
            return new RewardUpgrade(prev);
        }

        return new EmptyTowerUpgrade();
    }
}

