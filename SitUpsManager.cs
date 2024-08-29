using UnityEngine;
using TMPro;

public class SitUpsManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text caloriesBurnedText;
    public GameObject completionMessagePanel;
    private int currentScore;
    private int targetScore;
    private float caloriesBurned;
    private const float caloriesPerSitUp = 6f;

    void Start()
    {
        currentScore = PlayerPrefs.GetInt("SitUps", 0);
        targetScore = PlayerPrefs.GetInt("TargetSitUps", 0);
        caloriesBurned = PlayerPrefs.GetFloat("CaloriesBurned", 0f);

        UpdateScoreText();
        UpdateCaloriesText();
        ResetCompletionMessage();
    }

    void Update()
    {
        UpdateCaloriesText();  // Her karede kalori metin alanını güncelle
    }

    public void IncrementSitUpsScore()
    {
        currentScore++;
        PlayerPrefs.SetInt("SitUps", currentScore);
        PlayerPrefs.Save();
        UpdateScoreText();

        AddCalories(caloriesPerSitUp);

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
        CheckWeightLoss();
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

    private void CheckWeightLoss()
    {
        const float caloriesToLoseWeight = 1000f;
        if (caloriesBurned >= caloriesToLoseWeight)
        {
            int weight = PlayerPrefs.GetInt("Weight", 0);
            weight--;
            PlayerPrefs.SetInt("Weight", weight);
            PlayerPrefs.Save();

            caloriesBurned -= caloriesToLoseWeight;
            PlayerPrefs.SetFloat("CaloriesBurned", caloriesBurned);
            UpdateCaloriesText();

            Debug.Log("Tebrikler! 1 kilo kaybettiniz.");
        }
    }
}