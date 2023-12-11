using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Re-roller
/// <summary>
///     Interactable object that rerolls purchasable items in the shop
/// </summary>
public class Reroller : InteractObject {
    /// <summary>Multiplier for price increase every reroll</summary>
    const float MULTIPLIER = 1.5f;
    
    BoxCollider2D bc;
    
    void Awake() {
        bc = GetComponent<BoxCollider2D>();
    }
    
    public override void Interaction() {
        if (Shop.SpendCoins(Shop.RerollCost)) {
            Shop.RollItems();
            Shop.RerollCost = Mathf.CeilToInt(Shop.RerollCost * MULTIPLIER);
            StartCoroutine(Reset());
        }
    }
    
    /// <summary>Sets interact action text with new price</summary>
    public void SetPriceText() {
        itemAction = "Reroll: " + Shop.RerollCost + "G";
    }
    
    /// Reset    
    /// <summary>
    ///     Resets interaction collider so text updates after interaction
    /// </summary>
    IEnumerator Reset() {
        bc.enabled = false;
        yield return new WaitForSeconds(interactCooldown);
        bc.enabled = true;
    }
}
