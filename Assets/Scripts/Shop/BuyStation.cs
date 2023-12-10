using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Buy Station
/// <summary>
///     <see cref="MonoBehaviour"/> script for a buy station in the shop. Can
///     spawn a random purchasable item based on rarities
/// </summary>
public class BuyStation : MonoBehaviour {
    /// Item rarities
    const float SP_CHANCE = 0.4f;
    const float T3_CHANCE = 0.1f;
    const float T2_CHANCE = 0.3f;
    
    /// Item costs
    const int SP_COST = 40;
    const int T3_COST = 30;
    const int T2_COST = 20;
    const int T1_COST = 10;
    
    /// <summary>Pointer to PurchasableItem prefab</summary>
    [SerializeField] GameObject PurchasablePrefab;
    
    /// Lists of possible purchasable items by rarity
    [SerializeField] List<GameObject> T1Items = new();
    [SerializeField] List<GameObject> T2Items = new();
    [SerializeField] List<GameObject> T3Items = new();
    [SerializeField] List<GameObject> SpItems = new();
    
    /// Rotating values for spawning purchasable item
    GameObject Purchasable = null;
    int PriceToSet;
    
    void Awake() {
        Shop.AddBuyStation(this);
    }
    
    /// <summary>Generates a new random purchasable item</summary>
    public void GenerateItem() {
        // Destroy last purchasable
        if (Purchasable != null) Destroy(Purchasable);
        
        // Get new random item
        float roll = Random.Range(0f, 1f);
        GameObject item;
        if (!Shop.SpecialRolled && roll < SP_CHANCE) item = GetSpItem();
        else if (roll < T3_CHANCE) item = GetT3Item();
        else if (roll < T3_CHANCE + T2_CHANCE) item = GetT2Item();
        else item = GetT1Item();
        
        // Create new purchasable item and set info
        Purchasable = Instantiate(PurchasablePrefab, transform);
        Vector3 offset = new Vector3(0f, .1f, 0f);
        Purchasable.transform.position += offset;
        Purchasable.GetComponent<PurchasableItem>().SetItem(item, PriceToSet);
    }
    
    /// <summary>Gets a random item from <see cref="SpItems"/></summary>
    /// <returns>The rolled special item</returns>
    GameObject GetSpItem() {
        Shop.SpecialRolled = true;
        PriceToSet = SP_COST;
        return SpItems[Random.Range(0, SpItems.Count - 1)];
    }
    
    /// <summary>Gets a random item from <see cref="T3Items"/></summary>
    /// <returns>The rolled tier-3 item</returns>
    GameObject GetT3Item() {
        PriceToSet = T3_COST;
        return T3Items[Random.Range(0, T3Items.Count - 1)];
    }
    
    /// <summary>Gets a random item from <see cref="T2Items"/></summary>
    /// <returns>The rolled tier-2 item</returns>
    GameObject GetT2Item() {
        PriceToSet = T2_COST;
        return T2Items[Random.Range(0, T2Items.Count - 1)];
    }
    
    /// <summary>Gets a random item from <see cref="T1Items"/></summary>
    /// <returns>The rolled tier-1 item</returns>
    GameObject GetT1Item() {
        PriceToSet = T1_COST;
        return T1Items[Random.Range(0, T1Items.Count - 1)];
    }
}
