// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// /// <summary>Button behaviors for the shop scene</summary>
// public class ShopUIButtonFunctions : MonoBehaviour {
//     ///<summary>Build index for the main level scene</summary>
//     const int LEVEL_SCENE = 0;
    
//     /// Sell values for the ores
//     const int COPPER_VALUE = 1;
//     const int IRON_VALUE = 2;
//     const int GOLD_VALUE = 3;
//     const int CRYSTAL_VALUE = 4;
    
//     /// Purchase values for upgrades
//     const int EX_UPGRADE_1_COST = 10;
//     const int EX_UPGRADE_2_COST = 20;
    
//     /// Upgrade purchase status
//     public static bool ExUpgrade1Purchased { get; private set; } = false;
//     public static bool ExUpgrade2Purchased { get; private set; } = false;
    
//     /// <summary>Serialized pointer to coin count script</summary>
//     [SerializeField] CoinCount CCScript;
    
//     /// Sell Ores
//     /// <summary>
//     ///     "Sell Ores" button function<br/>
//     ///     Adds to coin count and sets ore counts to 0
//     /// </summary>
//     public void SellOres() {
//         // Add coins
//         InventorySystem.staticCoinsValue +=
//             InventorySystem.staticCopperValue * COPPER_VALUE +
//             InventorySystem.staticIronValue * IRON_VALUE +
//             InventorySystem.staticGoldValue * GOLD_VALUE +
//             InventorySystem.staticCrystalValue * CRYSTAL_VALUE
//         ;
        
//         // Update ore counts
//         InventorySystem.staticCopperValue = 0;
//         InventorySystem.staticIronValue = 0;
//         InventorySystem.staticGoldValue = 0;
//         InventorySystem.staticCrystalValue = 0;
        
//         // Update coin count in UI
//         CCScript.UpdateValue();
//     }
    
//     /// <summary>Placeholder button function or an example upgrade</summary>
//     public void ExampleUpgrade1() {
//         if (!ExUpgrade1Purchased && SpendCoins(EX_UPGRADE_1_COST)) {
//             ExUpgrade1Purchased = true;
//             print("Upgrade 1 purchased");
//             // Increase some stat or something else
//         }
//     }
    
//     /// <summary>Placeholder button function for an example upgrade</summary>
//     public void ExampleUpgrade2() {
//         if (!ExUpgrade2Purchased && SpendCoins(EX_UPGRADE_2_COST)) {
//             ExUpgrade2Purchased = true;
//             print("Upgrade 2 purchased");
//             // Increase some stat or something else
//         }
//     }
    
//     /// Next Level
//     /// <summary>
//     ///     "Next Level" button function<br/>
//     ///     Closes shop and moves to next level
//     /// </summary>
//     public void NextLevel() => SceneManager.LoadScene("CurrentScene");
    
//     /// Spend Coins
//     /// <summary>
//     ///     Removes coins from player's inventory if they are present
//     /// </summary>
//     /// <param name="amt">Amount of coins to remove</param>
//     /// <returns>Whether or not player has enough coins to remove</returns>
//     bool SpendCoins(int amt) {
//         if (InventorySystem.staticCoinsValue >= amt) {
//             InventorySystem.staticCoinsValue -= amt;
//             CCScript.UpdateValue();
//             return true;
//         }
//         return false;
//     }
// }