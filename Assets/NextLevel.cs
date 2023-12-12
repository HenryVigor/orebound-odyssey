using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour, IEducational
{
    public CavernGenerator cavernGenerator; // Reference to the CavernGenerator component
    private LevelIndicator levelIndicator; // Declare levelIndicator at the class level

    void Start()
    {
        levelIndicator = FindObjectOfType<LevelIndicator>();
    }

    const int shopFrequency = 3; // How often a shop will appear
    static bool playerAtShop = false; // Whether player is at the shop

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Obj && cavernGenerator != null)
        {
            if (collision.gameObject.GetComponent<PlayerInteract>().educationalMode == true)
            {
                GoNextLevel();
            }
            else
            {
                GoNextLevel();
            }
        }
    }

    public void GoNextLevel()
    {
        if (!playerAtShop && levelIndicator.LevelValue % shopFrequency == 0)
        {
            // Send player to shop
            Player.Obj.transform.position = new(150.5f, 51.5f, Player.Obj.transform.position.z);
            Instantiate(gameObject, new(150.5f, 49.5f, transform.position.z), Quaternion.identity);
            Shop.Initialize();
            playerAtShop = true;
        }
        else
        {
            Destroy(GameObject.Find("BlockHolder"));
            levelIndicator.LevelValue += 1; // Increment the level value, this will automatically update the UI text --- Needed to move this into here so that the value was incremented before generation (for getting floor theme)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMine>().playerScore += 1;
            cavernGenerator.GenerateCavern();
        }
        Destroy(gameObject);
    }

    public void EduAnswerCorrect()
    {
        Invoke("GoNextLevel", 1.25f);
    }

    public void EduAnswerIncorrect()
    {
        return;
    }
}