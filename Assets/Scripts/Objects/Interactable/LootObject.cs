using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : InteractObject
{

    [SerializeField] List<GameObject> possibleBasicItems = new List<GameObject>();
    [SerializeField] List<GameObject> possibleRareItems = new List<GameObject>();
    [SerializeField] List<GameObject> possibleSpecialItems = new List<GameObject>();
    [SerializeField] float rareDropChance = 15f;
    [SerializeField] float specialDropChance = 5f;
    [SerializeField] float lockedChance = 0f;
    private bool locked = false;

    private void Start()
    {
        float lockedCheck = Random.Range(1f, 100f);
        if (lockedCheck <= lockedChance)
        {
            locked = true;
        }
        if (locked)
        {
            itemText = "Locked";
            itemAction = "Unlock";
        }
    }

    public override void Interaction()
    {
        if (locked)
        {
            UnlockLootObject();
        }
        else
        {
            DropLoot();
            Destroy(gameObject);
        }
    }

    private void DropLoot()
    {
        float dropRarity = Random.Range(1f, 100f);
        if (dropRarity <= specialDropChance)
        {
            int dropChosen = Random.Range(0, possibleSpecialItems.Count);
            GameObject dropObject = possibleSpecialItems[dropChosen];
            Instantiate(dropObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
        }
        else if (dropRarity <= rareDropChance)
        {
            int dropChosen = Random.Range(0, possibleRareItems.Count);
            GameObject dropObject = possibleRareItems[dropChosen];
            Instantiate(dropObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
        }
        else
        {
            int dropChosen = Random.Range(0, possibleBasicItems.Count);
            GameObject dropObject = possibleBasicItems[dropChosen];
            Instantiate(dropObject, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
        }
    }

    private void UnlockLootObject()
    {
        // Programming Q here?
        Debug.Log("Loot Unlocked"); // for testing
        itemText = "Unlocked";
        itemAction = "Open";
        locked = false;
    }

}
