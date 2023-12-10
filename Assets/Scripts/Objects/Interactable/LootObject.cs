using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootObject : InteractObject, IEducational
{
    public GameObject breakParticles;
    [SerializeField] List<GameObject> possibleBasicItems = new List<GameObject>();
    [SerializeField] List<GameObject> possibleRareItems = new List<GameObject>();
    [SerializeField] List<GameObject> possibleSpecialItems = new List<GameObject>();
    [SerializeField] float rareDropChance = 15f;
    [SerializeField] float specialDropChance = 5f;
    [SerializeField] float lockedChance = 0f;
    private float itemDropChance = 100;
    private bool locked = false;
    public bool educationalMode = false;
    private InventorySystem inventorySystem;

    private void Start()
    {
        float lockedCheck = Random.Range(1f, 100f);
        if (lockedCheck <= lockedChance || educationalMode)
        {
            locked = true;
        }
        if (locked)
        {
            if (educationalMode)
            {
                itemText = "Locked";
                itemAction = "Unlock - Answer Question";
            }
            else
            {
                itemText = "Locked";
                itemAction = "Unlock - 1 Gold";
            }

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
            GameObject breakObj = Instantiate(breakParticles, transform.position, Quaternion.identity);
            breakObj.GetComponent<ParticleSystem>().Play();
            DropLoot();
            Destroy(gameObject);
        }
    }

    private void DropLoot()
    {
        float dropItem = Random.Range(1f, 100f);
        if (dropItem <= itemDropChance)
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
    }

    private bool CanPurchase(int invValue, int cost)
    {
        if (invValue >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetUnlockText()
    {
        itemText = "Unlocked";
        itemAction = "Open";
        var interact = GameObject.FindGameObjectWithTag("Player").transform.Find("InteractRange").gameObject;
        // This section is a temp way of refreshing the text
        interact.SetActive(false);
        interact.SetActive(true);
    }

    private void UnlockLootObject()
    {
        if (educationalMode)
        {
            var prompt = GameObject.FindGameObjectWithTag("Player").transform.Find("HUD").Find("EduPrompt");
            prompt.GetComponent<EducationQuestion>().targetObject = gameObject;
            prompt.gameObject.SetActive(true);
        }
        else
        {
            inventorySystem = FindObjectOfType<InventorySystem>();

            // Could be switched to keys later if implemented
            if (CanPurchase(inventorySystem.GoldValue, 1))
            {
                inventorySystem.GoldValue -= 1;
                locked = false;
            }

            if (!locked)
            {
                SetUnlockText();
            }

        }
    }

    public void EduAnswerCorrect()
    {
        locked = false;
        SetUnlockText();
    }

    public void EduAnswerIncorrect()
    {
        if (itemDropChance > 0)
        {
            itemDropChance -= 25f;
        }
    }

    public void ToggleEducational()
    {
        if (!educationalMode)
        {
            educationalMode = true;
        }
        else
        {
            educationalMode = false;
        }
    }
}
