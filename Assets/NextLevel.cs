using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    const int shopFrequency = 3; // How often a shop will appear
    const int shopScene = 1; // Name of shop scene
    
    static int currentLevel = 1; // Number of levels played
    
    public CavernGenerator cavernGenerator; // Reference to the CavernGenerator component

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Obj && cavernGenerator != null)
        {
            if (currentLevel % shopFrequency == 0)
            {
                SceneManager.LoadScene(shopScene);
            }
            else
            {
                Destroy(GameObject.Find("BlockHolder"));
                cavernGenerator.GenerateCavern();
            }
            
            currentLevel++;
            Destroy(gameObject);
        }
    }
}