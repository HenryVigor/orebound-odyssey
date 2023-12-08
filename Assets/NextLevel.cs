using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour, IEducational
{
    public CavernGenerator cavernGenerator; // Reference to the CavernGenerator component
    private SpriteRenderer cavernSpriteRenderer; // Reference to the Sprite Renderer of the cavern prefab
    private LevelIndicator levelIndicator; // Declare levelIndicator at the class level
    public GameObject trappedStoneBlock; // Reference to trap

    private Color[] predefinedColors = new Color[]
    {
    new Color(141f / 255f, 129f / 255f, 154f / 255f),   // Dark-blue
    new Color(215f / 255f, 195f / 255f, 114f / 255f),   // Yellowish
    new Color(214f / 255f, 154f / 255f, 114f / 255f),   // Orange-ish
    new Color(164f / 255f, 147f / 255f, 173f / 255f)   // Purple-ish
        // Add more predefined colors as needed
    };

    void Start()
    {
        levelIndicator = FindObjectOfType<LevelIndicator>();

        // Find the SpriteRenderer of the cavern prefab in generator script
        cavernSpriteRenderer = cavernGenerator.cavernSpritePrefab.GetComponent<SpriteRenderer>();
    }

    const int shopFrequency = 3; // How often a shop will appear
    const int shopScene = 1; // Shop scene index

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Obj && cavernGenerator != null)
        {
            if (collision.gameObject.GetComponent<PlayerInteract>().educationalMode == true)
            {
                var prompt = GameObject.FindGameObjectWithTag("Player").transform.Find("HUD").Find("EduPrompt");
                prompt.GetComponent<EducationQuestion>().targetObject = gameObject;
                prompt.transform.Find("DropText").gameObject.SetActive(false);
                prompt.gameObject.SetActive(true);
            }
            else
            {
                GoNextLevel();
            }
        }
    }

    public void GoNextLevel()
    {
        if (levelIndicator.LevelValue % shopFrequency == 0)
        {
            SceneManager.LoadScene("ShopScene");
            levelIndicator.LevelValue += 1; // Increment the level value, this will automatically update the UI text
        }
        else
        {
            Destroy(GameObject.Find("BlockHolder"));
            levelIndicator.LevelValue += 1; // Increment the level value, this will automatically update the UI text --- Needed to move this into here so that the value was incremented before generation (for getting floor theme)

            // Randomly select a color from predefinedColors and color cavernPrefab's sprite renderer
            int randomIndex = Random.Range(0, predefinedColors.Length);
            cavernSpriteRenderer.color = predefinedColors[randomIndex];
            trappedStoneBlock.GetComponent<SpriteRenderer>().color = predefinedColors[randomIndex]; //Change color of trap blocks as well

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