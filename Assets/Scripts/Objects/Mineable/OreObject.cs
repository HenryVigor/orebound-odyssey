using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreObject : BlockObject
{

    //
    // An extension of the generic block objects - drops items from a drop table when destroyed
    //
    private InventorySystem inventorySystem;

    [Header("Ore Drop Settings")]
    [SerializeField] bool hasDrops = true;
    [SerializeField] int dropCount = 1; // How many times drops should be rolled for
    [SerializeField] int baseDropValue = 1; // How much will be added to the inventory value by default before bonuses (default 1)
    public int dropBonus = 0; // A static bonus for ore drops (player upgrade) (default 0, no bonus)
    public float dropMultiplier = 1; // Multiplier for ore drops (player upgrade) (default 1, no bonus)
    [SerializeField] int oreID; // This is used to determine which inventory value should be incremented.
                                // 0 - Copper, 1 - Iron, 2 - Gold, 3 - Crystal

    //[SerializeField] List<GameObject> droppedItems = new List<GameObject>(); -- for gameobject dropping, currently not in use

    [Header("Drop Randomization Settings")]
    [SerializeField] bool randomizeDropGiven = false; // If the value of the ore should be randomized instead of a set value
    [SerializeField] int maxPossibleValue = 1; // The maximum value it could be randomized to
    [SerializeField] bool randomizeDropChance = false; // If drop chance should not always be 100%
    [SerializeField] [Range(0f, 100f)] protected float dropChance = 100f; // Chance to give a drop

    private void DropItems()
    {
        for (int i = 0; i < dropCount; i++)
        {
            float dropPercentage = dropChance; // Initialize ore drop chance to whatever (default 100% -- always drops)
            
            // Randomize Value (if enabled)
            if (randomizeDropGiven)
            {
                baseDropValue = Random.Range(1, maxPossibleValue);
            }

            // Randomize Drop Chance (if enabled)
            if (randomizeDropChance)
            {
                dropPercentage = Random.Range(0f, 100f);
            }

            // Add the ore value to inventory with modifiers (if drop chance success)
            if (dropPercentage <= dropChance)
            {
                //GameObject dropObject = droppedItems[dropChosen]; -- for dropping a gameobject upon ore break, however currently only incrementing inventory values
                //Debug.Log("Dropped " + dropObject.name);

                // Apply all potential modifiers from player upgrades
                baseDropValue = Mathf.FloorToInt(baseDropValue * dropMultiplier);
                baseDropValue += dropBonus;

                // Add to inventory
                AddOreValue();
            }
        }

    }

    private void AddOreValue()
    {
        inventorySystem = FindObjectOfType<InventorySystem>();
        
        switch(oreID)
        {
            case 0:
                inventorySystem.CopperValue += baseDropValue;
                break;
            case 1:
                inventorySystem.IronValue += baseDropValue;
                break;
            case 2:
                inventorySystem.GoldValue += baseDropValue;
                break;
            case 3:
                inventorySystem.CrystalValue += baseDropValue;
                break;
            default:
                inventorySystem.CopperValue += baseDropValue;
                break;
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
