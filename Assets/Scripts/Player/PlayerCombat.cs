using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Player Combat
/// <summary>
///     Singleton <see cref="MonoBehaviour"/> for managing player combat
/// </summary>
public class PlayerCombat : MonoBehaviour {
    /// <summary>Default length of invincibility time</summary>
    const float INVINCIBILITY_PERIOD = 0.5f;
    
    static PlayerCombat _Instance;
    /// <summary>Singleton <see cref="PlayerCombat"/> instance</summary>
    /// <value>Set only if null</value>
    static PlayerCombat Instance {
        get => _Instance;
        set => _Instance ??= value;
    }
    
    /// <summary>Maximum health value</summary>
    [SerializeField] int MaxHealth = 5;
    
    [SerializeField] int _Health;
    /// <summary>Current health value</summary>
    /// <value>Must remain between 0 and <see cref="MaxHealth"/></value>
    public static int Health {
        get => Instance._Health;
        private set {
            if (value > Instance.MaxHealth) value = Instance.MaxHealth;
            else if (value < 0) value = 0;
            Instance._Health = value;
        }
    }
    
    /// <summary>Invincibility status</summary>
    bool Invincible = false;
    
    void Awake() {
        Instance = this;
        Health = MaxHealth;
    }
    
    /// <summary>Registers a hit if player is not currently invincible</summary>
    /// <param name="damage">Amount of damage to deal to player</param>
    public static void Hit(int damage) {
        if (!Instance.Invincible) {
            // Subtract health
            Health -= damage;
            
            // Print result
            if (Health > 0) print("Health: " + Health);
            else print("You died");
            
            // Grant invincibility frames
            Instance.StartCoroutine(IFrames(INVINCIBILITY_PERIOD));
        }
    }
    
    /// I-Frames
    /// <summary>
    ///     Makes player invincible for <paramref name="time"/> seconds
    /// </summary>
    /// <param name="time">Number of seconds to make player invincible</param>
    static IEnumerator IFrames(float time) {
        Instance.Invincible = true;
        yield return new WaitForSeconds(time);
        Instance.Invincible = false;
    }
}