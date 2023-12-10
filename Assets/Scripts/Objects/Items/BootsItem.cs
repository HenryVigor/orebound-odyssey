using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsItem : BaseItem
{
    protected override void AddUpgrade()
    {
        PlayerCombat.spikeDamageImmune = true;
    }

}
