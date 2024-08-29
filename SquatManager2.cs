using UnityEngine;
using TMPro;

public class SquatManager2 : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text caloriesBurnedText;
    public GameObject completionMessagePanel;

    private int currentScore;
    private int targetScore;
    private float caloriesBurned;
    private const float caloriesPerSquat = 5f;

    void Start()
    {
        // Kullanıcı verilerini yükleyin
        LoadUserData();

        // Paneli başta gizleyin
        completionMessagePanel.SetActive(false);

        // Skoru ve kaloriyi güncelleyin
        UpdateScoreText();
        UpdateCaloriesText();
    }

    void Update()
    {
        // Her karede kalori değeri güncellenir
        UpdateCaloriesText();
    }

    public void IncrementSquatScore()
    {
        currentScore++;
        PlayerPrefs.SetInt("Squats", currentScore);
        PlayerPrefs.Save();
        UpdateScoreText();

        AddCalories(caloriesPerSquat); // Kalori artırma işlemi

        Debug.Log($"Current Score: {currentScore}, Target Score: {targetScore}"); // Skoru ve hedefi yazdır

        if (currentScore >= targetScore)
        {
            Debug.Log("Target reached, showing completion panel");
            ShowCompletionMessage();
        }
    
}

    private void AddCalories(float calories)
    {
        // Kalori ekleme işlemi
        caloriesBurned += calories;
        PlayerPrefs.SetFloat("CaloriesBurned", caloriesBurned);
        PlayerPrefs.Save();
        UpdateCaloriesText(); // Metin alanını güncelle
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{currentScore}/{targetScore}";
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
            completionMessagePanel.SetActive(true); // Paneli açar
            Time.timeScale = 0f; // Oyunu durdurur
            Debug.Log("Completion Panel Activated and Game Frozen"); // Debug logu
        }
    }

    private void LoadUserData()
    {
        currentScore = PlayerPrefs.GetInt("Squats", 0);
        targetScore = PlayerPrefs.GetInt("TargetSquats", 10); // AI Prediction'dan alınan değeri burada kullanın
        caloriesBurned = PlayerPrefs.GetFloat("CaloriesBurned", 0f);
    }
}