using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : InteractObject
{

    [Header("Item Information")]
    [SerializeField] private string itenName = "Test Item"; // Might try to find a way to display this upon pickup like a UI popup or something
    [SerializeField] private string itemText = "Test Description"; // Might try to find a way to display this upon pickup like a UI popup or something

    [Header("Player Health Upgrades")]
    [SerializeField] private bool upgradingHealth = false;
    [SerializeField] private int maxHealthBonus = 0;

    //[Header("Player Movement Upgrades")]
    //[SerializeField] private bool upgradingSpeed = false;
    //[SerializeField] private float speedBonus = 0f;

    [Header("Player Attack Upgrades")]
    [SerializeField] private bool upgradingAttack = false;
    [SerializeField] private float attackRateBonus = 0f;
    [SerializeField] private int attackDamageBonus = 0;
    [SerializeField] private float critChanceBonus = 0f;
    [SerializeField] private float critMultiplierBonus = 0f;
    [SerializeField] private float attackAreaXBonus = 0f;
    [SerializeField] private float attackAreaYBonus = 0f;

    [Header("Player Mining Upgrades")]
    [SerializeField] private bool upgradingMining = false;
    [SerializeField] private float mineSpeedBonus = 0f;
    [SerializeField] private int mineDamageBonus = 0;
    [SerializeField] private int mineOreBonus = 0;
    [SerializeField] private float oreMultiplierBonus = 0f;
    [SerializeField] private float mineAreaXBonus = 0f;
    [SerializeField] private float mineAreaYBonus = 0f;

    public override void Interaction()
    {
        AddUpgrade();
        Destroy(gameObject);
    }

    private void AddUpgrade()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (upgradingHealth)
        {
            PlayerCombat.Heal(maxHealthBonus);
        }
        //if (upgradingSpeed)
        //{
            // NYI
        //}
        if (upgradingAttack)
        {
            var playerAttack = player.GetComponent<PlayerMine>();
            var playerAttackRange = player.transform.Find("PlayerRange");
            playerAttack.attackRate += attackRateBonus;
            playerAttack.attackDamage += attackDamageBonus;
            playerAttack.critChance += critChanceBonus;
            playerAttack.critMultiplier += critMultiplierBonus;
            playerAttack.attackAreaX += attackAreaXBonus;
            playerAttack.attackAreaY += attackAreaYBonus;
            playerAttackRange.transform.position = new Vector2(playerAttackRange.transform.position.x + (attackAreaXBonus / 2), playerAttackRange.transform.position.y);
        }
        if (upgradingMining)
        {
            var playerMine = player.GetComponent<PlayerMine>();
            var playerMineRange = player.transform.Find("PlayerRange");
            playerMine.mineSpeed += mineSpeedBonus;
            playerMine.mineDamage += mineDamageBonus;
            playerMine.oreBonus += mineOreBonus;
            playerMine.oreMultiplier += oreMultiplierBonus;
            playerMine.mineAreaX += mineAreaXBonus;
            playerMine.mineAreaY += mineAreaYBonus;
            playerMineRange.transform.position = new Vector2(playerMineRange.transform.position.x + (attackAreaXBonus / 2), playerMineRange.transform.position.y);
        }
        Debug.Log(itemText);
    }

}
