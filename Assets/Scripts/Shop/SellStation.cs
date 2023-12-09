using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Sell Station
/// <summary>
///     Interactable object that will sell all of a given type of ore from the
///     player's inventory
/// </summary>
public class SellStation : InteractObject {
    /// Ore values
    const int COAL_VALUE = 1;
    const int COPPER_VALUE = 1;
    const int IRON_VALUE = 2;
    const int GOLD_VALUE = 3;
    const int CRYSTAL_VALUE = 4;
    
    /// <summary>Ore types</summary>
    enum Ore {Coal, Copper, Iron, Gold, Crystal, All};
    
    /// <summary>Type of ore this station will sell</summary>
    [SerializeField] Ore OreType;
    
    /// <summary>Pointer to player's inventory system</summary>
    InventorySystem Inv;
    
    void Start() {
        Inv = FindObjectOfType<InventorySystem>();
    }
    
    public override void Interaction() {
        switch (OreType) {
            case Ore.Coal: {
                SellCoal();
                break;
            }
            case Ore.Copper: {
                SellCopper();
                break;
            }
            case Ore.Iron: {
                SellIron();
                break;
            }
            case Ore.Gold: {
                SellGold();
                break;
            }
            case Ore.Crystal: {
                SellCrystal();
                break;
            }
            case Ore.All: {
                SellAll();
                break;
            }
        }
    }
    
    /// <summary>Replaces player's coal with its worth in coins</summary>
    void SellCoal() {
        Inv.CoinsValue += Inv.CoalValue * COAL_VALUE;
        Inv.CoalValue = 0;
        
    }
    
    /// <summary>Replaces player's copper with its worth in coins</summary>
    void SellCopper() {
        Inv.CoinsValue += Inv.CopperValue * COPPER_VALUE;
        Inv.CopperValue = 0;
    }
    
    /// <summary>Replaces player's iron with its worth in coins</summary>
    void SellIron() {
        Inv.CoinsValue += Inv.IronValue * IRON_VALUE;
        Inv.IronValue = 0;
    }
    
    /// <summary>Replaces player's gold with its worth in coins</summary>
    void SellGold() {
        Inv.CoinsValue += Inv.GoldValue * GOLD_VALUE;
        Inv.GoldValue = 0;
    }
    
    /// <summary>Replaces player's crystal with its worth in coins</summary>
    void SellCrystal() {
        Inv.CoinsValue += Inv.CrystalValue * CRYSTAL_VALUE;
        Inv.CrystalValue = 0;
    }
    
    /// <summary>Calls sell functions for all ore types</summary>
    void SellAll() {
        SellCoal();
        SellCopper();
        SellIron();
        SellGold();
        SellCrystal();
    }
}
