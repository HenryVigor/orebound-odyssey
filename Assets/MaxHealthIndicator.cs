using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaxHealthIndicator : MonoBehaviour {
    TextMeshProUGUI TextField;
    
    void Awake() {
        TextField = GetComponent<TextMeshProUGUI>();
    }

    public void Set(int maxHealth) {
        if (maxHealth > 0) TextField.text = "<b>" + maxHealth + "</b>"; 
        else TextField.text = "<b>0</b>";
    }
}
