using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Static class for managing the shop</summary>
public static class Shop {
    /// Initial Reroll Multiplier
    /// <summary>
    ///     Multiplier for starting reroll cost on shop initialization<br/>
    ///     Based on current level value
    /// </summary>
    const int INIT_REROLL_MULTIPLIER = 3;
    
    /// Special Rolled
    /// <summary>
    ///     Whether a special item has been rolled. Set to false at start of
    ///     item roll. Set to true if special is rolled.
    /// </summary>
    public static bool SpecialRolled = false;
    
    /// <summary>List of pointers to existing buy stations in the shop</summary>
    static List<BuyStation> BuyStations = new();
    
    static int _RerollCost;
    /// <summary>Cost to reroll shop items</summary>
    /// <value>Sets re-roller object's interact text on set</value>
    public static int RerollCost {
        get => _RerollCost;
        set {
            _RerollCost = value;
            Rlr.SetPriceText();
        }
    }
    
    /// <summary>Pointer to player's inventory system</summary>
    static InventorySystem Inv = Object.FindObjectOfType<InventorySystem>();
    
    /// <summary>Pointer to level indicator</summary>
    /// <typeparam name="LevelIndicator"></typeparam>
    /// <returns></returns>
    static LevelIndicator Lev = Object.FindObjectOfType<LevelIndicator>();
    
    /// <summary>Pointer to re-roller object</summary>
    static Reroller Rlr = Object.FindObjectOfType<Reroller>();
    
    /// <summary>Adds a buy station to the list</summary>
    /// <param name="station">Buy station to add</param>
    public static void AddBuyStation(BuyStation station) {
        BuyStations.Add(station);
    }
    
    public static void Initialize() {
        RollItems();
        RerollCost = Lev.LevelValue * INIT_REROLL_MULTIPLIER;
    }
    
    /// <summary>Re-rolls purchasable items in shop</summary>
    public static void RollItems() {
        SpecialRolled = false;
        foreach (BuyStation station in BuyStations) station.GenerateItem();
    }
    
    /// Spend Coins
    /// <summary>
    ///     Removes coins from player's inventory if they are present
    /// </summary>
    /// <param name="amt">Amount of coins to remove</param>
    /// <returns>Whether or not player has enough coins to remove</returns>
    public static bool SpendCoins(int amt) {
        if (Inv.CoinsValue >= amt) {
            Inv.CoinsValue -= amt;
            return true;
        }
        return false;
    }
}
