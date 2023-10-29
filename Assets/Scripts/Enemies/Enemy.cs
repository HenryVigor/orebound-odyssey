using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject == Player.Obj) PlayerCombat.Hit(1);

    }
}
