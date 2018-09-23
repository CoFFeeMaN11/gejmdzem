using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EmptyTowerUpgrade : ITowerUpgrade
{
    public void Effect(TowerScript tower)
    {
        Debug.Log("Effect actived");
    }
}

