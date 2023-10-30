using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : InteractObject
{

    public override void Interaction()
    {
        Debug.Log("Dropped loot! (NYI)");
        Destroy(gameObject);
    }

}
