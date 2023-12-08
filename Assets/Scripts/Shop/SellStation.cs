using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellStation : InteractObject {
    const int COAL_VALUE = 1;
    const int COPPER_VALUE = 1;
    const int IRON_VALUE = 2;
    const int GOLD_VALUE = 3;
    const int CRYSTAL_VALUE = 4;
    
    enum Ore {Coal, Copper, Iron, Gold, Crystal, All};
    
    [SerializeField] Ore OreType;
    
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
    
    void SellCoal() {
        Inv.CoinsValue += Inv.CoalValue * COAL_VALUE;
        Inv.CoalValue = 0;
        
    }
    
    void SellCopper() {
        Inv.CoinsValue += Inv.CopperValue * COPPER_VALUE;
        Inv.CopperValue = 0;
    }
    
    void SellIron() {
        Inv.CoinsValue += Inv.IronValue * IRON_VALUE;
        Inv.IronValue = 0;
    }
    
    void SellGold() {
        Inv.CoinsValue += Inv.GoldValue * GOLD_VALUE;
        Inv.GoldValue = 0;
    }
    
    void SellCrystal() {
        Inv.CoinsValue += Inv.CrystalValue * CRYSTAL_VALUE;
        Inv.CrystalValue = 0;
    }
    
    void SellAll() {
        SellCoal();
        SellCopper();
        SellIron();
        SellGold();
        SellCrystal();
    }
}
