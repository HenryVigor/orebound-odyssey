using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject cratePrefabA;
    public GameObject cratePrefabB;
    public GameObject cratePrefabC;
    public GameObject playerPrefab;
    // Reset the Educational Mode bool on lootboxes and player to False upon Menu appearing
    // Needed since the bool is set in the prefab itself so it seemingly carries over, can desync the toggle and bool
    private void Awake()
    {
        cratePrefabA.GetComponent<LootObject>().educationalMode = false;
        cratePrefabB.GetComponent<LootObject>().educationalMode = false;
        cratePrefabC.GetComponent<LootObject>().educationalMode = false;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
