using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public CavernGenerator cavernGenerator; // Reference to the CavernGenerator component
    private LevelIndicator levelIndicator; // Declare levelIndicator at the class level

    void Start()
    {
        levelIndicator = FindObjectOfType<LevelIndicator>();
    }

    const int shopFrequency = 3; // How often a shop will appear
    const int shopScene = 1; // Shop scene index

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Obj && cavernGenerator != null)
        {
            if (levelIndicator.LevelValue % shopFrequency == 0)
            {
                SceneManager.LoadScene(shopScene);
                levelIndicator.LevelValue += 1; // Increment the level value, this will automatically update the UI text
            }
            else
            {
                Destroy(GameObject.Find("BlockHolder"));
                levelIndicator.LevelValue += 1; // Increment the level value, this will automatically update the UI text --- Needed to move this into here so that the value was incremented before generation (for getting floor theme)
                cavernGenerator.GenerateCavern();
            }
            Destroy(gameObject);
        }
    }
}