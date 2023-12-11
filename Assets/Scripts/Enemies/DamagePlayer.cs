using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public float enemyKnockback = 7f;

    public string enemyName;

    public AIChase aIChase;

    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject == Player.Obj)
        {
            // Hit player
            PlayerCombat.Hit(1, enemyName);
            // Knockback
            Vector2 diff = (transform.position - collision.transform.position).normalized;
            Vector2 force = diff * enemyKnockback;
            collision.rigidbody.AddForce(-force, ForceMode2D.Impulse);

            if (enemyName == "Goblin")
            {
                aIChase.speed *= -1;
            }
        }
    }
}
