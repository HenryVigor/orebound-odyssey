using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthIndicator : MonoBehaviour {
    TextMeshProUGUI TextField;
    
    void Awake() {
        TextField = GetComponent<TextMeshProUGUI>();
    }
    
    public void Set(int health) {
        if (health > 0) TextField.text = "<b>Health: " + health + "</b>"; 
        else TextField.text = "<b>You Died!</b>";
    }
}
