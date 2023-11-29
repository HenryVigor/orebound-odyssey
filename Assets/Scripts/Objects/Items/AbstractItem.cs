using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractItem : InteractObject
{

    // Abstract base class for items/upgrades

    [Header("Player Health Upgrades")]
    [SerializeField] protected bool upgradingHealth = false;
    [SerializeField] protected float healBonus = 0;
    [SerializeField] protected float maxHealthBonus = 0;

    //[Header("Player Movement Upgrades")]
    //[SerializeField] private bool upgradingSpeed = false;
    //[SerializeField] private float speedBonus = 0f;

    [Header("Player Attack Upgrades")]
    [SerializeField] protected bool upgradingAttack = false;
    [SerializeField] protected float attackRateBonus = 0f;
    [SerializeField] protected float attackDamageBonus = 0;
    [SerializeField] protected float critChanceBonus = 0f;
    [SerializeField] protected float critMultiplierBonus = 0f;
    [SerializeField] protected float attackAreaXBonus = 0f;
    [SerializeField] protected float attackAreaYBonus = 0f;

    [Header("Player Mining Upgrades")]
    [SerializeField] protected bool upgradingMining = false;
    [SerializeField] protected float mineSpeedBonus = 0f;
    [SerializeField] protected float mineDamageBonus = 0;
    [SerializeField] protected float mineOreBonus = 0;
    [SerializeField] protected float oreMultiplierBonus = 0f;
    [SerializeField] protected float mineAreaXBonus = 0f;
    [SerializeField] protected float mineAreaYBonus = 0f;

    public override abstract void Interaction();

    protected abstract void AddUpgrade();

}
