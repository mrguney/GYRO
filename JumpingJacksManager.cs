using UnityEngine;
using TMPro;

public class JumpingJacksManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text caloriesBurnedText;
    public GameObject completionMessagePanel;

    private int currentScore;
    private int targetScore;
    private float caloriesBurned;
    private const float caloriesPerJumpingJack = 8f;

    void Start()
    {
        currentScore = PlayerPrefs.GetInt("JumpingJacks", 0);
        targetScore = PlayerPrefs.GetInt("TargetJumpingJacks", 0);
        caloriesBurned = PlayerPrefs.GetFloat("CaloriesBurned", 0f);

        UpdateScoreText();
        UpdateCaloriesText();
        ResetCompletionMessage();
    }

    void Update()
    {
        UpdateCaloriesText();  // Her karede kalori metin alanını güncelle
    }

    public void IncrementJumpingJacksScore()
    {
        currentScore++;
        PlayerPrefs.SetInt("JumpingJacks", currentScore);
        PlayerPrefs.Save();
        UpdateScoreText();

        AddCalories(caloriesPerJumpingJack);

        if (currentScore >= targetScore)
        {
            ShowCompletionMessage();
        }
    }

    private void AddCalories(float calories)
    {
        caloriesBurned += calories;
        PlayerPrefs.SetFloat("CaloriesBurned", caloriesBurned);
        PlayerPrefs.Save();
        UpdateCaloriesText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    private void UpdateCaloriesText()
    {
        if (caloriesBurnedText != null)
        {
            caloriesBurnedText.text = PlayerPrefs.GetFloat("CaloriesBurned", 0f).ToString("F2");
        }
    }

    private void ShowCompletionMessage()
    {
        if (completionMessagePanel != null)
        {
            completionMessagePanel.SetActive(true);
        }
    }

    private void ResetCompletionMessage()
    {
        if (completionMessagePanel != null)
        {
            completionMessagePanel.SetActive(false);
        }
    }
}