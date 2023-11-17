using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public CavernGenerator cavernGenerator; // Reference to the CavernGenerator component
    private LevelIndicator levelIndicator; // Declare levelIndicator at the class level

    void Start()
    {
        levelIndicator = FindObjectOfType<LevelIndicator>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Obj && cavernGenerator != null)
        {
            Destroy(GameObject.Find("BlockHolder"));
            cavernGenerator.GenerateCavern();
            Destroy(gameObject);
            levelIndicator.LevelValue += 1; // Increment the level value, this will automatically update the UI text
        }
    }
}