using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class EducationQuestion : MonoBehaviour
{

    /// The current questions in place were either written myself or found on:
    /// www.sanfoundry.com/computer-science-questions-answers/
    /// www.sanfoundry.com/1000-computer-fundamentals-questions-answers/
    /// www.includehelp.com/mcq/computer-science-quiz.aspx
    /// www.javatpoint.com/data-structure-mcq
    /// www.proprofs.com/quiz-school/story.php?title=computer-programming-ii-exam-multiple-choice
    /// www.proprofs.com/quiz-school/story.php?title=multiple-choice-questions_53gu

    [System.Serializable]
    public class Question
    {
        public string text;
        public string answerA;
        public string answerB;
        public string answerC;
        public string answerD;
        public string correctAnswer;
    }

    [System.Serializable]
    public class Questions
    {
        public Question[] questions;
    }

    public TextAsset questionBankJSON;

    public GameObject targetObject;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI optionAText;
    [SerializeField] TextMeshProUGUI optionBText;
    [SerializeField] TextMeshProUGUI optionCText;
    [SerializeField] TextMeshProUGUI optionDText;
    [SerializeField] TextMeshProUGUI dropText;
    [SerializeField] Image menuImage;
    [SerializeField] Image aButtonImage;
    [SerializeField] Image bButtonImage;
    [SerializeField] Image cButtonImage;
    [SerializeField] Image dButtonImage;
    private int dropChance;
    private string correctAnswer;
    public int questionAnsweredCount = 0;
    private int currentAttemptCount = 0;

    Questions questionBank = new Questions();

    private void Awake()
    {
        // Create question bank from JSON
        string jsonPath = Application.streamingAssetsPath + "/QuestionBank.txt";
        string jsonContents = File.ReadAllText(jsonPath);
        questionBank = JsonUtility.FromJson<Questions>(jsonContents);
    }

    private void OnEnable()
    {
        StartCoroutine(FadeIn(0.125f));
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        dropChance = 100;
        currentAttemptCount = 0;

        // Pick an answer from the bank
        int questionIndex = UnityEngine.Random.Range(0, questionBank.questions.Length);
        Question questionSelected = questionBank.questions[questionIndex];
        List<string> questionAnswers = new List<string>() { questionSelected.answerA, questionSelected.answerB, questionSelected.answerC, questionSelected.answerD };
        correctAnswer = questionSelected.correctAnswer;
        
        // Shuffle answers and assign values to text, reset formatting
        questionText.text = questionSelected.text;
        Debug.Log(questionAnswers.Count);
        questionAnswers = ShuffleAnswers(questionAnswers);
        Debug.Log(questionAnswers.Count);
        optionAText.text = questionAnswers[0];
        optionAText.color = new(0, 0, 0, 0);
        optionAText.fontStyle = FontStyles.Normal;
        optionBText.text = questionAnswers[1];
        optionBText.color = new(0, 0, 0, 0);
        optionBText.fontStyle = FontStyles.Normal;
        optionCText.text = questionAnswers[2];
        optionCText.color = new(0, 0, 0, 0);
        optionCText.fontStyle = FontStyles.Normal;
        optionDText.text = questionAnswers[3];
        optionDText.color = new(0, 0, 0, 0);
        optionDText.fontStyle = FontStyles.Normal;
        dropText.text = "Item Drop Chance: " + dropChance + "%";
    }

    private List<string> ShuffleAnswers(List<string> questionAnswers)
    {
        int index = questionAnswers.Count;
        System.Random rand = new System.Random();
        while (index > 1)
        {
            index--;
            int randIndex = rand.Next(index + 1);
            string val = questionAnswers[randIndex];
            questionAnswers[randIndex] = questionAnswers[index];
            questionAnswers[index] = val;
        }
        return questionAnswers;
    }

    public void SelectAnswer()
    {

        TextMeshProUGUI answerSelected = EventSystem.current.currentSelectedGameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        if (answerSelected.text == correctAnswer)
        {
            // Change UI
            answerSelected.color = new Color(0, 185, 0);
            // Close after a second
            StartCoroutine(FadeOut(0.5f, true));
        }
        else
        {
            // Change UI
            answerSelected.color = new Color(185, 0, 0);
            answerSelected.fontStyle = FontStyles.Strikethrough;
            // Apply fail result
            currentAttemptCount++;
            targetObject.GetComponent<IEducational>().EduAnswerIncorrect();
            if (dropChance > 0)
            {
                dropChance -= 25;
                dropText.text = "Item Drop Chance: " + dropChance + "%";
            }
        }

        // This is for exiting just in case a question in the JSON is typed wrong and there is no correct answer possible
        if (currentAttemptCount > 5)
        {
            StartCoroutine(FadeOut(0.5f, false));
        }

    }

    private void ClosePrompt()
    {
        gameObject.SetActive(false);
        transform.Find("DropText").gameObject.SetActive(true);
    }

    private IEnumerator FadeIn(float time)
    {
        float timeCount = 0;
        while (timeCount < time)
        {
            float t = timeCount / time;
            menuImage.color = Color.Lerp(new(0, 0, 0, 0), new(0, 0, 0, 125f / 255f), t);
            questionText.color = Color.Lerp(new(1, 1, 1, 0), new(1, 1, 1, 1), t);
            dropText.color = Color.Lerp(new(1, 1, 1, 0), new(1, 1, 1, 1), t);
            aButtonImage.color = Color.Lerp(new(1, 1, 1, 0), new(1, 1, 1, 1), t);
            bButtonImage.color = Color.Lerp(new(1, 1, 1, 0), new(1, 1, 1, 1), t);
            cButtonImage.color = Color.Lerp(new(1, 1, 1, 0), new(1, 1, 1, 1), t);
            dButtonImage.color = Color.Lerp(new(1, 1, 1, 0), new(1, 1, 1, 1), t);
            optionAText.color = Color.Lerp(new(0, 0, 0, 0), new(0, 0, 0, 1), t);
            optionBText.color = Color.Lerp(new(0, 0, 0, 0), new(0, 0, 0, 1), t);
            optionCText.color = Color.Lerp(new(0, 0, 0, 0), new(0, 0, 0, 1), t);
            optionDText.color = Color.Lerp(new(0, 0, 0, 0), new(0, 0, 0, 1), t);
            timeCount += Time.deltaTime;
            yield return null;
        }
        menuImage.color = new(0, 0, 0, 125f / 255f);
        questionText.color = new(1, 1, 1, 1);
        dropText.color = new(1, 1, 1, 1);
        aButtonImage.color = new(1, 1, 1, 1);
        bButtonImage.color = new(1, 1, 1, 1);
        cButtonImage.color = new(1, 1, 1, 1);
        dButtonImage.color = new(1, 1, 1, 1);
    }

    private IEnumerator FadeOut(float time, bool correct)
    {
        yield return 0.35f;
        float timeCount = 0;
        while (timeCount < time)
        {
            float t = timeCount / time;
            menuImage.color = Color.Lerp(new(0, 0, 0, 125f / 255f), new(0, 0, 0, 0), t);
            questionText.color = Color.Lerp(new(1, 1, 1, 1), new(1, 1, 1, 0), t);
            dropText.color = Color.Lerp(new(1, 1, 1, 1), new(1, 1, 1, 0), t);
            aButtonImage.color = Color.Lerp(new(1, 1, 1, 1), new(1, 1, 1, 0), t);
            bButtonImage.color = Color.Lerp(new(1, 1, 1, 1), new(1, 1, 1, 0), t);
            cButtonImage.color = Color.Lerp(new(1, 1, 1, 1), new(1, 1, 1, 0), t);
            dButtonImage.color = Color.Lerp(new(1, 1, 1, 1), new(1, 1, 1, 0), t);
            optionAText.color = Color.Lerp(optionAText.color, new(optionAText.color.r, optionAText.color.g, optionAText.color.b, 0), t);
            optionBText.color = Color.Lerp(optionBText.color, new(optionBText.color.r, optionBText.color.g, optionBText.color.b, 0), t);
            optionCText.color = Color.Lerp(optionCText.color, new(optionCText.color.r, optionCText.color.g, optionCText.color.b, 0), t);
            optionDText.color = Color.Lerp(optionDText.color, new(optionDText.color.r, optionDText.color.g, optionDText.color.b, 0), t);
            timeCount += Time.deltaTime;
            yield return null;
        }
        menuImage.color = new(0, 0, 0, 0);
        questionText.color = new(0, 0, 0, 0);
        dropText.color = new(1, 1, 1, 0);
        aButtonImage.color = new(1, 1, 1, 0);
        bButtonImage.color = new(1, 1, 1, 0);
        cButtonImage.color = new(1, 1, 1, 0);
        dButtonImage.color = new(1, 1, 1, 0);
        ClosePrompt();
        if (correct)
        {
            // Apply success results
            targetObject.GetComponent<IEducational>().EduAnswerCorrect();
            questionAnsweredCount++;
        }
    }

}
