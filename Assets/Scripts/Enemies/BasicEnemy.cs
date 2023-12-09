using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : BaseEnemy
{
    public float knockback;

    public float stunRate = 0.15f;

    private AIChase aiChase;

    // Sound
    public EnemyAudio enemyAudio;

    private void Start()
    {
        currentHealth = maxHealth;

        aiChase = GetComponent<AIChase>();
    }

    public override void Damage(int damageAmount)
    {
        if (isKillable)
        {
            currentHealth -= damageAmount;
            // Knockback
            transform.position = Vector2.MoveTowards(this.transform.position, Player.Obj.transform.position, -1 * knockback * Time.deltaTime);
            // Stun
            aiChase.canMove = false;
            Invoke("ResetMove", stunRate);
        }
        // Play hurt sound
        if (enemyAudio != null) {
            enemyAudio.PlaySoundHurt();
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

    private void ResetMove()
    {
        if (aiChase != null)
        {
            aiChase.canMove = true;
        }
        else
        {
            Debug.LogError("AIChase component not found!");
        }
    }

}
