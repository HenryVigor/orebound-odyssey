using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Singleton <see cref="MonoBehaviour"/> for HUD elements</summary>
public class HUD : MonoBehaviour {
    /// <summary>Static pointer to HUD'S health indicator script</summary>
    public static HealthIndicator HIndicator { get; private set; }
    
    static HUD _Instance;
    /// <summary>Singleton HUD instance</summary>
    /// <value>Set only if null</value>
    static HUD Instance {
        get => _Instance;
        set => _Instance ??= value;
    }
    
    void Awake() {
        Instance = this;
        if (Instance == this) {
            // Get children
            foreach (Transform child in transform) {
                if (child.name == "Health Indicator") {
                    HIndicator = child.GetComponent<HealthIndicator>();
                }
            }
        }
    }
}
