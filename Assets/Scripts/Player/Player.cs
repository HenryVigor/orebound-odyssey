using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Player
/// <summary>
///     Singleton <see cref="MonoBehaviour"/> class that ensures only one player
///     object is loaded
/// </summary>
public class Player : MonoBehaviour {
    /// <summary>Pointer to player <see cref="GameObject"/></summary>
    /// <value>Set on <see cref="Instance"/> set</value>
    public static GameObject Obj { get; private set; }
    
    static Player _Instance;
    /// <summary>Singleton <see cref="Player"/> instance</summary>
    /// <value>Set if null</value>
    static Player Instance {
        get => _Instance;
        set {
            if (_Instance == null) {
                _Instance = value;
                Obj = value.gameObject;
            }
        }
    }
    
    void Awake() {
        Instance = this;
        if (Instance != this) Destroy(gameObject);
        // else DontDestroyOnLoad(gameObject);
    }
    
    void OnDestroy() {
        _Instance = null;
    }
}