using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreObject : BlockObject
{

    //
    // An extension of the generic block objects - drops items from a drop table when destroyed
    //

    [Header("Ore Drop Settings")]
    [SerializeField] bool hasDrops = true;
    [SerializeField] int dropCount = 1;
    [SerializeField] List<GameObject> droppedItems = new List<GameObject>();

    [Header("Drop Randomization Settings")]
    [SerializeField] bool randomizeDropGiven = false;
    [SerializeField] bool randomizeDropChance = false;
    [SerializeField] [Range(0f, 100f)] protected float dropChance = 100f;

    private void DropItems()
    {
        for (int i = 0; i < dropCount; i++)
        {
            int dropChosen = i;
            float dropPercentage = dropChance;

            if (randomizeDropGiven)
            {
                dropChosen = Random.Range(0, droppedItems.Count);
            }
            if (randomizeDropChance)
            {
                dropPercentage = Random.Range(0f, 100f);
            }

            if (dropPercentage <= dropChance)
            {
                GameObject dropObject = droppedItems[dropChosen];
                Debug.Log("Dropped " + dropObject.name);
            }
        }
    }

    public override void ObjectDestroy()
    {

        if (hasDrops)
        {
            DropItems();
        }

        base.ObjectDestroy();
    }

}
