using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMine : MonoBehaviour
{

    PlayerInput pi;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private LayerMask mineableLayers;
    public Transform attackPoint;

    // Set which tool the player is using
    public bool usePickaxe = true;

    // Attack Variables
    [Header("Player Attack Settings")]
    public float attackRate = 0.45f; // Cooldown between attacks
    public float attackDamage = 10; // Base damage done to enemies
    public float critChance = 0f; // Chance for a critical hit (player upgrade)
    public float critMultiplier = 1.5f; // Critical damage multiplier (player upgrade)
    public float attackAreaX = 0.4f;
    public float attackAreaY = 0.3f;
    private bool canAttack = true;

    // Mining Variables
    [Header("Player Mining Settings")]
    public float mineSpeed = 0.2f; // Cooldown between mining
    public float mineDamage = 10; // Base damage done to ore durability
    public float oreBonus = 0; // Ore drop value static bonus (player upgrade) (default 0, no bonus)
    public float oreMultiplier = 1; // Ore drop value multiplier (player upgrade) (default 1, no bonus)
    public float mineAreaX = 0.65f;
    public float mineAreaY = 0.3f;
    private bool canMine = true;

    // Tool animation
    public Animator toolAnimator;

    // Sound
    public PlayerAudioScript playerAudioScript;

    private void Start()
    {
        Transform toolAnimatorObject = transform.Find("Tool");
        if (toolAnimatorObject != null)
        {
        toolAnimator = toolAnimatorObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("oops");
        }
    }

    private void Awake()
    {
        pi = GetComponent<PlayerInput>();

        
    }

    private void Update()
    {
        // Update current tool
        if (pi.actions["SelectPickaxe"].IsPressed()) {
            usePickaxe = true;
        }
        if (pi.actions["SelectSword"].IsPressed()) {
            usePickaxe = false;
        }
        // Update animator
        toolAnimator.SetBool("UsePickaxe", usePickaxe);

        if (pi.actions["Attack"].IsPressed()) Attack();
    }

    private void Attack()
    {
        // Attack any enemies in the attack range
        if (canAttack && usePickaxe == false)
        {
            // Animation
            toolAnimator.SetBool("Using Tool", true);

            canAttack = false;

            Collider2D[] hitEntities = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackAreaX, attackAreaY), attackPoint.eulerAngles.z, enemyLayers);

            foreach(Collider2D entity in hitEntities)
            {
                float critical = Random.Range(1f, 100f);

                if (critical <= critChance)
                {
                    entity.GetComponent<IDamageable>().Damage(Mathf.FloorToInt(attackDamage * critMultiplier));
                }
                else
                {
                    entity.GetComponent<IDamageable>().Damage(Mathf.FloorToInt((attackDamage)));
                }

            }

            Invoke("ResetUsingTool", 0.2f);
            Invoke("ResetAttack", attackRate);
        }

        // Mine any blocks in the mining range
        if (canMine && usePickaxe == true)
        {
            // Animation
            toolAnimator.SetBool("Using Tool", true);

            canMine = false;

            Collider2D[] hitEntities = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(mineAreaX, mineAreaY), attackPoint.eulerAngles.z, mineableLayers);

            foreach (Collider2D entity in hitEntities)
            {
                entity.GetComponent<IDamageable>().Damage(Mathf.FloorToInt((mineDamage)));
                if (entity.tag == "OreBlock")
                {
                    entity.GetComponent<OreObject>().dropBonus = Mathf.FloorToInt(oreBonus);
                    entity.GetComponent<OreObject>().dropMultiplier = oreMultiplier;
                }

                // Play mine sound
                if (playerAudioScript != null) {
                    playerAudioScript.PlaySoundMine();
                }
            }

            Invoke("ResetUsingTool", 0.2f);
            Invoke("ResetMine", mineSpeed);
        }

    }

    private void ResetUsingTool()
    {
        toolAnimator.SetBool("Using Tool", false);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void ResetMine()
    {
        canMine = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireCube(attackPoint.position, new Vector2(attackAreaX, attackAreaY));
        Gizmos.DrawWireCube(attackPoint.position, new Vector2(mineAreaX, mineAreaY));
    }

}
