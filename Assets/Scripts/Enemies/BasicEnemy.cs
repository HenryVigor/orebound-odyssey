using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : BaseEnemy
{

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public override void Damage(int damageAmount)
    {
        if (isKillable)
        {
            currentHealth -= damageAmount;
        }
        if (currentHealth <= 0)
        {
            ObjectDestroy();
        }
    }

    public override void ObjectDestroy()
    {
        Destroy(gameObject);
    }

}
