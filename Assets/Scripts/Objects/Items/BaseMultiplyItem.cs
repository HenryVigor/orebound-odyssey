using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMultiplyItem : BaseItem
{

    // Percentage upgrade items - the set bonus values will be MULTIPLIED with the player's current stats (with any floor values being rounded down to meet any integer stats, after multiplicaton happens)
    // E.g. attack damage bonus = 1.3f, new attack damage for player will be the floored integer of (attack damage * 1.3f)
    // This is so stats can be increased by %, like increasing attack rate by 50% of the current rate instead of a set increase amount

    // NOTE, unaffected stats should still be set to 1 to avoid any changes

    protected override void AddUpgrade()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (upgradingHealth)
        {
            PlayerCombat.UpgradeMaxHealth(Mathf.CeilToInt(PlayerCombat.GetMaxHealth() * maxHealthBonus));
            PlayerCombat.Heal(Mathf.CeilToInt(PlayerCombat.Health * healBonus));
        }
        //if (upgradingSpeed)
        //{
        // NYI
        //}
        if (upgradingAttack)
        {
            var playerAttack = player.GetComponent<PlayerMine>();
            var playerAttackRange = player.transform.Find("PlayerRange");
            playerAttack.attackRate *= attackRateBonus;
            playerAttack.attackDamage *= attackDamageBonus;
            playerAttack.critChance *= critChanceBonus;
            playerAttack.critMultiplier *= critMultiplierBonus;
            playerAttack.attackAreaX *= attackAreaXBonus;
            playerAttack.attackAreaY *= attackAreaYBonus;
            playerAttackRange.transform.position = new Vector2(playerAttackRange.transform.position.x + (attackAreaXBonus / 2), playerAttackRange.transform.position.y);
        }
        if (upgradingMining)
        {
            var playerMine = player.GetComponent<PlayerMine>();
            var playerMineRange = player.transform.Find("PlayerRange");
            playerMine.mineSpeed *= mineSpeedBonus;
            playerMine.mineDamage *= mineDamageBonus;
            playerMine.oreBonus *= mineOreBonus;
            playerMine.oreMultiplier *= oreMultiplierBonus;
            playerMine.mineAreaX *= mineAreaXBonus;
            playerMine.mineAreaY *= mineAreaYBonus;
            playerMineRange.transform.position = new Vector2(playerMineRange.transform.position.x + (attackAreaXBonus / 2), playerMineRange.transform.position.y);
        }
    }

}
