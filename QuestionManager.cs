using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public Button[] answerButtons;
    public GameObject quizCompletePanel; // Quiz bitiş paneli
    public Color correctAnswerColor = Color.green;
    public Color wrongAnswerColor = Color.red;
    public Color defaultColor = Color.white;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private List<Question> questions = new List<Question>();
    private float timePerQuestion = 10f;
    private float currentTimer;

    void Start()
    {
        quizCompletePanel.SetActive(false); // Paneli başlangıçta gizle
        LoadQuestions();
        DisplayQuestion();
        UpdateScoreText();
        currentTimer = timePerQuestion;
    }

    void Update()
    {
        if (currentQuestionIndex < questions.Count)
        {
            currentTimer -= Time.deltaTime;
            timerText.text = $"Time: {Mathf.Ceil(currentTimer)}";

            if (currentTimer <= 0)
            {
                TimeOut();
            }
        }
    }

    private void LoadQuestions()
    {
        questions.Add(new Question(
            "What is the most effective method to significantly reduce your daily sugar intake without feeling deprived?",
            "Fruits",
            new string[] { "Fruits", "Diet Soda", "Skip Desserts", "Sugar-Free Gum" },
            "Healthy Eating"
        ));

        questions.Add(new Question(
            "How many glasses of water should an adult drink daily for optimal health?",
            "8",
            new string[] { "6", "8", "4", "10" }, // Doğru cevap B butonunda
            "Hydration"
        ));

        questions.Add(new Question(
            "Which meal is considered the most important of the day for a healthy metabolism?",
            "Breakfast",
            new string[] { "Dinner", "Lunch", "Breakfast", "Snack" }, // Doğru cevap C butonunda
            "Nutrition"
        ));

        questions.Add(new Question(
            "What is the recommended daily intake of fiber for an average adult?",
            "25g",
            new string[] { "25g", "15g", "30g", "20g" }, // Doğru cevap A butonunda
            "Nutrition"
        ));

        questions.Add(new Question(
            "Which type of fat is considered the healthiest and beneficial for heart health?",
            "Unsaturated",
            new string[] { "Trans Fat", "Saturated", "Cholesterol", "Unsaturated" }, // Doğru cevap D butonunda
            "Healthy Eating"
        ));

        questions.Add(new Question(
            "How many hours of sleep are recommended for an adult to maintain a healthy weight?",
            "7-9",
            new string[] { "7-9", "5-6", "6-7", "8-10" }, // Doğru cevap A butonunda
            "Sleep"
        ));

        questions.Add(new Question(
            "What is the most effective way to lose weight sustainably?",
            "Balanced Diet",
            new string[] { "Balanced Diet", "Fad Diets", "Skipping Meals", "Supplements" }, // Doğru cevap A butonunda
            "Weight Loss"
        ));

        questions.Add(new Question(
            "Which nutrient is essential for building and repairing tissues in the body?",
            "Protein",
            new string[] { "Vitamins", "Carbohydrates", "Fat", "Protein" }, // Doğru cevap D butonunda
            "Nutrition"
        ));

        questions.Add(new Question(
            "How many portions of fruits and vegetables should you eat daily?",
            "5",
            new string[] { "5", "3", "4", "6" }, // Doğru cevap A butonunda
            "Healthy Eating"
        ));

        questions.Add(new Question(
            "What type of exercise is most effective for burning fat?",
            "Cardio",
            new string[] { "Flexibility", "Strength", "Yoga", "Cardio" }, // Doğru cevap D butonunda
            "Exercise"
        ));
    }

    private void DisplayQuestion()
    {
        ResetButtons();
        currentTimer = timePerQuestion;

        if (currentQuestionIndex < questions.Count)
        {
            Question question = questions[currentQuestionIndex];
            questionText.text = question.Text;
            for (int i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.Options[i];
                int answerIndex = i;
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerIndex));
            }
        }
        else
        {
            quizCompletePanel.SetActive(true); // Quiz bittiğinde paneli göster
            questionText.text = "Quiz Complete!";
            foreach (Button button in answerButtons)
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    private void OnAnswerSelected(int index)
    {
        Question currentQuestion = questions[currentQuestionIndex];
        string selectedAnswer = answerButtons[index].GetComponentInChildren<TMP_Text>().text;

        if (selectedAnswer == currentQuestion.CorrectAnswer)
        {
            answerButtons[index].image.color = correctAnswerColor;
            score++;
            UpdateScoreText();
            Invoke("NextQuestion", 1f);
        }
        else
        {
            answerButtons[index].image.color = wrongAnswerColor;
        }
    }

    private void NextQuestion()
    {
        currentQuestionIndex++;
        DisplayQuestion();
    }

    private void TimeOut()
    {
        currentQuestionIndex++;
        DisplayQuestion();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    private void ResetButtons()
    {
        foreach (Button button in answerButtons)
        {
            button.image.color = defaultColor;
            button.interactable = true;
        }
    }
}

[System.Serializable]
public class Question
{
    public string Text;
    public string CorrectAnswer;
    public string[] Options;
    public string Category;

    public Question(string text, string correctAnswer, string[] options, string category)
    {
        Text = text;
        CorrectAnswer = correctAnswer;
        Options = options;
        Category = category;
    }
}