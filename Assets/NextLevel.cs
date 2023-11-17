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
    const int shopScene = 1; // Name of shop scene   

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Obj && cavernGenerator != null)
        {
            if (levelIndicator.LevelValue % shopFrequency == 0)
            {
                SceneManager.LoadScene(shopScene);
            }
            else
            {
                Destroy(GameObject.Find("BlockHolder"));
                cavernGenerator.GenerateCavern();
            }
            
            levelIndicator.LevelValue += 1; // Increment the level value, this will automatically update the UI text
            Destroy(gameObject);
        }
    }
}