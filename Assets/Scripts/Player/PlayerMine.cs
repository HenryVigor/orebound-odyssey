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

    // Attack Variables
    [Header("Player Attack Settings")]
    public float attackRate = 0.5f; // Cooldown between attacks
    public int attackDamage = 10; // Base damage done to enemies
    public float critChance = 0f; // Chance for a critical hit (player upgrade)
    public int critMultiplier = 2; // Critical damage multiplier (player upgrade)
    public float attackAreaX = 0.6f;
    public float attackAreaY = 0.5f;
    private bool canAttack = true;

    // Mining Variables
    [Header("Player Mining Settings")]
    public float mineSpeed = 0.5f; // Cooldown between mining
    public int mineDamage = 10; // Base damage done to ore durability
    public int oreBonus = 0; // Ore drop value static bonus (player upgrade) (default 0, no bonus)
    public int oreMultiplier = 1; // Ore drop value multiplier (player upgrade) (default 1, no bonus)
    public float mineAreaX = 0.6f;
    public float mineAreaY = 0.5f;
    private bool canMine = true;

    private void Awake()
    {
        pi = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (pi.actions["Attack"].IsPressed()) Attack();
    }

    private void Attack()
    {
        // Attack any enemies in the attack range
        if (canAttack)
        {
            canAttack = false;

            Collider2D[] hitEntities = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(attackAreaX, attackAreaY), attackPoint.eulerAngles.z, enemyLayers);

            foreach(Collider2D entity in hitEntities)
            {
                entity.GetComponent<IDamageable>().Damage(attackDamage);
            }

            Invoke("ResetAttack", attackRate);
        }

        // Mine any blocks in the mining range
        if (canMine)
        {
            canMine = false;

            Collider2D[] hitEntities = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(mineAreaX, mineAreaY), attackPoint.eulerAngles.z, mineableLayers);

            foreach (Collider2D entity in hitEntities)
            {
                entity.GetComponent<IDamageable>().Damage(mineDamage);
                if (entity.tag == "OreBlock")
                {
                    entity.GetComponent<OreObject>().dropBonus = oreBonus;
                    entity.GetComponent<OreObject>().dropMultiplier = oreMultiplier;
                }

            }

            Invoke("ResetMine", mineSpeed);
        }

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
