using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCount : MonoBehaviour {
    TextMeshProUGUI TextField;
    
    void Awake() {
        TextField = GetComponent<TextMeshProUGUI>();
        UpdateValue();
    }
    
    public void UpdateValue() {
        TextField.text =
            "<b>Coins: " + InventorySystem.staticCoinsValue + "</b>"
        ;
    }
}
