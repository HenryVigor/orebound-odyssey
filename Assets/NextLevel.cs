using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour, IEducational
{
    public CavernGenerator cavernGenerator; // Reference to the CavernGenerator component
    //private SpriteRenderer cavernSpriteRenderer; // Reference to the Sprite Renderer of the cavern prefab
    private LevelIndicator levelIndicator; // Declare levelIndicator at the class level
    //public GameObject trappedStoneBlock; // Reference to trap

    //private Color[] predefinedColors = new Color[]
    //{
    //new Color(141f / 255f, 129f / 255f, 154f / 255f),   // Dark-blue
    //new Color(215f / 255f, 195f / 255f, 114f / 255f),   // Yellowish
    //new Color(214f / 255f, 154f / 255f, 114f / 255f),   // Orange-ish
    //new Color(164f / 255f, 147f / 255f, 173f / 255f)   // Purple-ish
    //    // Add more predefined colors as needed
    //};

    void Start()
    {
        levelIndicator = FindObjectOfType<LevelIndicator>();

        // Find the SpriteRenderer of the cavern prefab in generator script
        //cavernSpriteRenderer = cavernGenerator.cavernSpritePrefab.GetComponent<SpriteRenderer>();
    }

    const int shopFrequency = 3; // How often a shop will appear
    static bool playerAtShop = false; // Whether player is at the shop

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