using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    void OnTriggerStay2D(Collider2D collider) {
        if (
            collider.gameObject == Player.Obj &&
            collider.GetComponent<Rigidbody2D>().velocity.magnitude >= 0.3f
        ) {
            PlayerCombat.Hit(1);
        }
    }
}
