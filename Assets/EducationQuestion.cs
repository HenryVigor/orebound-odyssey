using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

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
    private int dropChance;
    private string correctAnswer;
    private int questionAnsweredCount = 0;
    private int currentAttemptCount = 0;

    Questions questionBank = new Questions();

    private void Awake()
    {
        // Create question bank from JSON
        questionBank = JsonUtility.FromJson<Questions>(questionBankJSON.text);
    }

    private void OnEnable()
    {
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
        optionAText.color = new Color(0, 0, 0);
        optionAText.fontStyle = FontStyles.Normal;
        optionBText.text = questionAnswers[1];
        optionBText.color = new Color(0, 0, 0);
        optionBText.fontStyle = FontStyles.Normal;
        optionCText.text = questionAnswers[2];
        optionCText.color = new Color(0, 0, 0);
        optionCText.fontStyle = FontStyles.Normal;
        optionDText.text = questionAnswers[3];
        optionDText.color = new Color(0, 0, 0);
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
            // Apply success results
            targetObject.GetComponent<IEducational>().EduAnswerCorrect();
            questionAnsweredCount++;
            // Close after a second
            Invoke("ClosePrompt", 1.25f);
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
            Invoke("ClosePrompt", 0.5f);
        }

    }

    private void ClosePrompt()
    {
        gameObject.SetActive(false);
        transform.Find("DropText").gameObject.SetActive(true);
    }

}
