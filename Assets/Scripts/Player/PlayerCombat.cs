using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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

    private static bool _spikeDamageImmune = false;
    public static bool spikeDamageImmune
    {
        get => _spikeDamageImmune;
        set
        {
            _spikeDamageImmune = value;
        }
    }

    /// <summary>Invincibility status</summary>
    bool Invincible = false;

    // Sound
    public PlayerAudio playerAudio;

    // PostProcessing for Damage Vignette Effect
    private Volume vol;
    private Vignette vignette;

    // Player movement
    public PlayerMovement playerMovement;
    public float movementPenaltyCooldown = 2f;

    // Inventory
    private InventorySystem inventorySystem;
    

    void Awake() {
        Instance = this;
        vol = GameObject.FindGameObjectWithTag("PostProcess").GetComponent<Volume>();
        vol.profile.TryGet(out vignette);
        Health = MaxHealth;
    }
    
    void OnDestroy() {
        _Instance = null;
    }
    
    /// <summary>Registers a hit if player is not currently invincible</summary>
    /// <param name="damage">Amount of damage to deal to player</param>
    public static void Hit(int damage, string enemyName) {
        if (!Instance.Invincible) {
            // Damage Vignette
            Instance.StartCoroutine(VignetteHit(0.175f));
            // Subtract health
            Health -= damage;
            // Update health indicator
            HUD.HIndicator.Set(Health);
            // Grant invincibility frames
            Instance.StartCoroutine(IFrames(INVINCIBILITY_PERIOD));

            if (enemyName == "Spider")
            {
                Instance.playerMovement.ExternalSpeedModifier = 0.5f;
                Instance.Invoke("ResetPlayerSpeed", Instance.movementPenaltyCooldown);
            }
            if (enemyName == "Goblin")
            {
                Instance.inventorySystem = FindObjectOfType<InventorySystem>();
                if (Instance.inventorySystem.CoinsValue < 10)
                {
                    Instance.inventorySystem.CoinsValue -= Instance.inventorySystem.CoinsValue;
                } else {
                    Instance.inventorySystem.CoinsValue -= 10;
                }
            }

            // Play hurt sound
            if (Instance.playerAudio != null) 
            {
                Instance.playerAudio.PlaySoundHurt();
            }
        }
    }
    
    /// <summary>Heals the player by <paramref name="amt"/> health</summary>
    /// <param name="amt">Amount of health to heal</param>
    public static void Heal(int amt) {
        // Add health
        Health += amt;
        if (Health > GetMaxHealth())
        {
            Health = GetMaxHealth();
        }
        
        // Update health indicator
        HUD.HIndicator.Set(Health);
    }

    /// <summary>Upgrades the player's max health to <paramref name="amt"/> health</summary>
    /// <param name="amt">Amount of health to set</param>
    public static void UpgradeMaxHealth(int amt)
    {
        // Add health
        Instance.MaxHealth = amt;
        HUD.MaxHIndicator.Set(GetMaxHealth());
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

    /// Vignette Effect
    /// <summary>
    ///     Flash a red vignette on screen for <paramref name="time"/> *2 seconds
    /// </summary>
    /// <param name="time">Time for vignette effect to appear/dissapear</param>
    static IEnumerator VignetteHit(float time)
    {
        float timeCount = 0;
        while (timeCount < time)
        {
            float t = timeCount / time;
            Instance.vignette.intensity.value = Mathf.Lerp(0f, 0.45f, t);
            timeCount += Time.deltaTime;
            yield return null;
        }
        Instance.vignette.intensity.value = 0.45f;
        timeCount = 0;
        while (timeCount < time)
        {
            float t = timeCount / time;
            Instance.vignette.intensity.value = Mathf.Lerp(0.45f, 0f, t);
            timeCount += Time.deltaTime;
            yield return null;
        }
        Instance.vignette.intensity.value = 0f;
    }

    private void ResetPlayerSpeed()
    {
        Instance.playerMovement.ExternalSpeedModifier = 1f;
    }
}