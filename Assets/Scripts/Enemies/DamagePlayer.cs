using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

    public float enemyKnockback = 7f;
    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject == Player.Obj)
        {
            PlayerCombat.Hit(1);
            // Knockback
            Vector2 diff = (transform.position - collision.transform.position).normalized;
            Vector2 force = diff * enemyKnockback;
            collision.rigidbody.AddForce(-force, ForceMode2D.Impulse);
        }
    }
}
