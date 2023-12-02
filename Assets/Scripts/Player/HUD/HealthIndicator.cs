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
        if (health > 0) TextField.text = "<b>" + health + "</b>"; 
        else TextField.text = "<b>0</b>";
    }
}
