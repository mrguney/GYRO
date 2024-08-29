using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text squatsScoreText;
    public TMP_Text jumpingJacksScoreText;
    public TMP_Text sitUpsScoreText;
    public TMP_Text pushUpsScoreText;
    public TMP_Text caloriesBurnedText;
    public TMP_Text currentWeightText;

    private int squatsScore;
    private int jumpingJacksScore;
    private int sitUpsScore;
    private int pushUpsScore;
    private float caloriesBurned;

    private const float caloriesPerSquat = 5f;
    private const float caloriesPerJumpingJack = 8f;
    private const float caloriesPerSitUp = 6f;
    private const float caloriesPerPushUp = 9f;

    private const float caloriesToLoseWeight = 1000f; // 1 kilo kaybı için gerekli kalori miktarı

    private void Start()
    {
        // Puan ve kalori bilgilerini yükle
        squatsScore = PlayerPrefs.GetInt("Squats", 0);
        jumpingJacksScore = PlayerPrefs.GetInt("JumpingJacks", 0);
        sitUpsScore = PlayerPrefs.GetInt("SitUps", 0);
        pushUpsScore = PlayerPrefs.GetInt("PushUps", 0);
        caloriesBurned = PlayerPrefs.GetFloat("CaloriesBurned", 0f); // Dikkat: Her yerde aynı anahtar kullanılmalı

        UpdateAllScoreTexts();
        UpdateCaloriesText();
        UpdateWeightText();
    }

    public void IncrementScore(string exerciseType)
    {
        // Hangi egzersizin yapıldığını belirleyip puan ve kaloriyi artır
        switch (exerciseType)
        {
            case "Squats":
                squatsScore++;
                PlayerPrefs.SetInt("Squats", squatsScore);
                AddCalories(caloriesPerSquat);
                break;
            case "JumpingJacks":
                jumpingJacksScore++;
                PlayerPrefs.SetInt("JumpingJacks", jumpingJacksScore);
                AddCalories(caloriesPerJumpingJack);
                break;
            case "SitUps":
                sitUpsScore++;
                PlayerPrefs.SetInt("SitUps", sitUpsScore);
                AddCalories(caloriesPerSitUp);
                break;
            case "PushUps":
                pushUpsScore++;
                PlayerPrefs.SetInt("PushUps", pushUpsScore);
                AddCalories(caloriesPerPushUp);
                break;
        }

        PlayerPrefs.Save();
        UpdateAllScoreTexts();
    }

    private void AddCalories(float calories)
    {
        // Kaloriyi ekleyip kontrol et
        caloriesBurned += calories;
        PlayerPrefs.SetFloat("CaloriesBurned", caloriesBurned); // Dikkat: Burada da aynı anahtar kullanılıyor
        UpdateCaloriesText();
        CheckWeightLoss();
    }

    private void UpdateCaloriesText()
    {
        if (caloriesBurnedText != null)
        {
            caloriesBurnedText.text = caloriesBurned.ToString("F2");
        }
    }

    private void CheckWeightLoss()
    {
        if (caloriesBurned >= caloriesToLoseWeight)
        {
            int weight = PlayerPrefs.GetInt("Weight", 0);
            weight--;
            PlayerPrefs.SetInt("Weight", weight);
            PlayerPrefs.Save();

            caloriesBurned -= caloriesToLoseWeight; // Kalan kaloriyi güncelle
            PlayerPrefs.SetFloat("CaloriesBurned", caloriesBurned);
            UpdateCaloriesText();
            UpdateWeightText();

            Debug.Log("Tebrikler! 1 kilo kaybettiniz.");
        }
    }

    private void UpdateWeightText()
    {
        if (currentWeightText != null)
        {
            currentWeightText.text = PlayerPrefs.GetInt("Weight", 0).ToString();
        }
    }

    private void UpdateAllScoreTexts()
    {
        if (squatsScoreText != null)
            squatsScoreText.text = $"{squatsScore}/{PlayerPrefs.GetInt("TargetSquats", 0)}";
        if (jumpingJacksScoreText != null)
            jumpingJacksScoreText.text = $"{jumpingJacksScore}/{PlayerPrefs.GetInt("TargetJumpingJacks", 0)}";
        if (sitUpsScoreText != null)
            sitUpsScoreText.text = $"{sitUpsScore}/{PlayerPrefs.GetInt("TargetSitUps", 0)}";
        if (pushUpsScoreText != null)
            pushUpsScoreText.text = $"{pushUpsScore}/{PlayerPrefs.GetInt("TargetPushUps", 0)}";
    }

    public void ResetScores()
    {
        PlayerPrefs.SetInt("Squats", 0);
        PlayerPrefs.SetInt("JumpingJacks", 0);
        PlayerPrefs.SetInt("SitUps", 0);
        PlayerPrefs.SetInt("PushUps", 0);
        PlayerPrefs.SetFloat("CaloriesBurned", 0f);
        PlayerPrefs.Save();

        squatsScore = 0;
        jumpingJacksScore = 0;
        sitUpsScore = 0;
        pushUpsScore = 0;
        caloriesBurned = 0f;

        UpdateAllScoreTexts();
        UpdateCaloriesText();
        UpdateWeightText();
    }
}