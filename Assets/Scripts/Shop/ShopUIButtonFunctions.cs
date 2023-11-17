using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopUIButtonFunctions : MonoBehaviour {
    const int LEVEL_SCENE = 0;
    const int COPPER_VALUE = 1;
    const int IRON_VALUE = 2;
    const int GOLD_VALUE = 3;
    const int CRYSTAL_VALUE = 4;
    const int EX_UPGRADE_1_COST = 10;
    const int EX_UPGRADE_2_COST = 20;
    
    public static bool ExUpgrade1Purchased { get; private set; } = false;
    public static bool ExUpgrade2Purchased { get; private set; } = false;
    
    public void SellOres() {
        print("Ores sold");
        /*/////  Pseudo code until inventory / currency is done  ///////////////
        inventory.coins +=
            inventory.copper * COPPER_VALUE +
            inventory.iron * IRON_VALUE +
            inventory.gold * GOLD_VALUE +
            inventory.crystal * CRYSTAL_VALUE
        ;
        
        inventory.copper = 0;
        inventory.iron = 0;
        inventory.gold = 0;
        inventory.crystal = 0;
        
        inventory.updateText();
        *///////////////////////////////////////////////////////////////////////
    }
    
    public void ExampleUpgrade1() {
        if (!ExUpgrade1Purchased && SpendCoins(EX_UPGRADE_1_COST)) {
            ExUpgrade1Purchased = true;
            print("Upgrade 1 purchased");
            // Increase some stat or something else
        }
    }
    
    public void ExampleUpgrade2() {
        if (!ExUpgrade2Purchased && SpendCoins(EX_UPGRADE_2_COST)) {
            ExUpgrade2Purchased = true;
            print("Upgrade 2 purchased");
            // Increase some stat or something else
        }
    }
    
    public void NextLevel() => SceneManager.LoadScene(LEVEL_SCENE);
    
    bool SpendCoins(int amt) {
        print("Spent " + amt + " coins");
        return true;
        /*/////  Pseudo code until inventory / currency is done  ///////////////
        if (inventory.coins >= amt) {
            inventory.coins -= amt;
            return true;
        }
        return false;
        *///////////////////////////////////////////////////////////////////////
    }
}