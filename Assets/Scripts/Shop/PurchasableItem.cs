using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Interactable shop item that requires purchasing for use</summary>
public class PurchasableItem : BaseItem {
    /// <summary>Pointer to prefab for the item to be purchased</summary>
    GameObject Item;
    
    /// <summary>Price required to purchase item</summary>
    int Price;

    public override void Interaction() {
        if (Shop.SpendCoins(Price)) {
            GameObject purchasedItem = Instantiate(Item, transform.parent);
            purchasedItem.GetComponent<InteractObject>().Interaction();
            Destroy(gameObject);
        }
    }
    
    /// Set Item
    /// <summary>
    ///     Sets item to be purchased and copies its interaction info
    /// </summary>
    /// <param name="item">
    ///     Pointer to prefab for the item to be purchased
    /// </param>
    /// <param name="price">Price required to purchase item</param>
    public void SetItem(GameObject item, int price) {
        Item = item;
        Price = price;
        GetComponent<SpriteRenderer>().sprite =
            Item.GetComponent<SpriteRenderer>().sprite
        ;
        InteractObject itemIO = Item.GetComponent<InteractObject>();
        itemName = itemIO.GetInteractName();
        itemText = itemIO.GetInteractDesc();
        itemAction = "Buy: " + Price + "G";
    }
}
