using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMine : MonoBehaviour
{

    PlayerInput pi;
    [SerializeField] private LayerMask damageableLayers;

    public bool canAttack = true;
    public float attackRate = 0.5f;
    private float currentAttackRate;
    public int attackDamage = 10;
    public Transform attackPoint;
    public Vector2 attackArea = new Vector2(1, .75f);

    private void Awake()
    {
        pi = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        currentAttackRate = attackRate;
        if (pi.actions["Attack"].IsPressed()) Attack();
    }

    private void Attack()
    {
        if (canAttack)
        {
            currentAttackRate = attackRate;
            canAttack = false;

            Collider2D[] hitEntities = Physics2D.OverlapBoxAll(attackPoint.position, attackArea, 0f, damageableLayers);

            foreach(Collider2D entity in hitEntities)
            {
                entity.GetComponent<IDamageable>().Damage(attackDamage);

                // Currently set to double attack rate if hitting a block; probably will be changed into something else later mainly just for testing
                if (entity.tag == "OreBlock" || entity.tag == "Mineable")
                {
                    currentAttackRate /= 2;
                }

            }

            Invoke("ResetAttack", currentAttackRate);
        }
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireCube(attackPoint.position, attackArea);
    }

}
