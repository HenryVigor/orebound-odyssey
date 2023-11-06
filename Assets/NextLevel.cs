using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public CavernGenerator cavernGenerator; // Reference to the CavernGenerator component

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Obj && cavernGenerator != null)
        {
            cavernGenerator.GenerateCavern();
        }
    }
}