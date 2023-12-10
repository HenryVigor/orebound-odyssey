using UnityEngine;

/// <summary>Behavior for spike trap object</summary>
public class Spikes : MonoBehaviour {
    void OnTriggerStay2D(Collider2D collider) {
        if (
            collider.gameObject == Player.Obj &&
            collider.GetComponent<Rigidbody2D>().velocity.magnitude >= 0.3f
        ) {
            if (PlayerCombat.spikeDamageImmune == false)
            PlayerCombat.Hit(1);
        }
    }
}
