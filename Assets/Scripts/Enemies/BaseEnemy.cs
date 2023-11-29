using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IDamageable
{

    //
    // Abstract class that serves a base for all enemy objects
    //

    [Header("Enemy Health Settings")]
    [SerializeField] protected bool isKillable = true;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth = 100;

    //[Header("Enemy Attack Settings")]
    //[SerializeField] protected int attackDamage = 1;
    //[SerializeField] protected float attackCooldown = 0.5f;
    //[SerializeField] protected bool canAttack = true;

    //[Header("Enemy Movement Settings")]
    //[SerializeField] protected float speed = 5f;

    public abstract void Damage(int damageAmount);
    public abstract void ObjectDestroy();

    //public abstract void Attack();

}
