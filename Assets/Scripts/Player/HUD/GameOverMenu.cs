using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameOverMenu : MonoBehaviour
{

    [SerializeField] Image menuImage;
    [SerializeField] Image backButtonImage;
    [SerializeField] Image quitButtonImage;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI backText;
    [SerializeField] TextMeshProUGUI quitText;
    [SerializeField] GameObject player;
    private void OnEnable()
    {
        if (player.GetComponent<PlayerInteract>().educationalMode == true)
        {
            scoreText.text = "Educational Questions Correct: " + transform.parent.Find("EduPrompt").GetComponent<EducationQuestion>().questionAnsweredCount;
        }
        else
        {
            scoreText.text = "Score: " + player.GetComponent<PlayerMine>().playerScore;
        }
        StartCoroutine(FadeIn(0.75f));
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("CurrentScene2");
    }

    public void BackToMenu()
    {
        Shop.ResetShop();
        SceneManager.LoadScene("MainMenu2");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeIn(float time)
    {
        float timeCount = 0;
        while (timeCount < time)
        {
            float t = timeCount / time;
            menuImage.color = Color.Lerp(new(0, 0, 0, 0), new(0, 0, 0, 125f/255f), t);
            gameOverText.color = Color.Lerp(new(168 / 255f, 26 / 255f, 26 / 255f, 0), new(168 / 255f, 26 / 255f, 26 / 255f, 1), t);
            scoreText.color = Color.Lerp(new(1, 1, 1, 0), new(1, 1, 1, 1), t);
            backButtonImage.color = Color.Lerp(new(1, 1, 1, 0), new(1, 1, 1, 1), t);
            quitButtonImage.color = Color.Lerp(new(1, 1, 1, 0), new(1, 1, 1, 1), t);
            backText.color = Color.Lerp(new(0, 0, 0, 0), new(0, 0, 0, 1), t);
            quitText.color = Color.Lerp(new(0, 0, 0, 0), new(0, 0, 0, 1), t);
            timeCount += Time.deltaTime;
            yield return null;
        }
        menuImage.color = new(0, 0, 0, 125f / 255f);
        gameOverText.color = new(168 / 255f, 26 / 255f, 26 / 255f, 1);
        scoreText.color = new(1, 1, 1, 1);
        backButtonImage.color = new(1, 1, 1, 1);
        quitButtonImage.color = new(1, 1, 1, 1);
        backText.color = new(0, 0, 0, 1);
        quitText.color = new(0, 0, 0, 1);
    }

}
