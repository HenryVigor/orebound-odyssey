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
    
    [SerializeField] static int _Health;
    /// <summary>Current health value</summary>
    /// <value>Must remain between 0 and <see cref="MaxHealth"/></value>

    public static int Health {
        get => _Health;
        private set {
            if (value > Instance.MaxHealth) value = Instance.MaxHealth;
            else if (value < 0) value = 0;
            _Health = value;
        }
    }
    
    /// <summary>Invincibility status</summary>
    bool Invincible = false;
    
    // Sound
    public PlayerAudioScript playerAudioScript;
    

    void Awake() {
        Instance = this;
        Health = MaxHealth;
    }
    
    void OnDestroy() {
        _Instance = null;
    }
    
    /// <summary>Registers a hit if player is not currently invincible</summary>
    /// <param name="damage">Amount of damage to deal to player</param>
    public static void Hit(int damage) {
        if (!Instance.Invincible) {
            // Subtract health
            Health -= damage;
            
            // Update health indicator
            HUD.HIndicator.Set(Health);
            
            // Grant invincibility frames
            Instance.StartCoroutine(IFrames(INVINCIBILITY_PERIOD));

            // Play hurt sound
            if (Instance.playerAudioScript != null) {
                Instance.playerAudioScript.PlaySoundHurt();
            }
        }
    }
    
    /// <summary>Heals the player by <paramref name="amt"/> health</summary>
    /// <param name="amt">Amount of health to heal</param>
    public static void Heal(int amt) {
        // Add health
        Health += amt;
        
        // Update health indicator
        HUD.HIndicator.Set(Health);
    }

    /// <summary>Upgrades the player's max health to <paramref name="amt"/> health</summary>
    /// <param name="amt">Amount of health to set</param>
    public static void UpgradeMaxHealth(int amt)
    {
        // Add health
        Instance.MaxHealth = amt;
    }

    /// <summary>Get the player's max health</summary>
    public static int GetMaxHealth()
    {
        return Instance.MaxHealth;
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