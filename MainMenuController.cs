using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MainMenuController : MonoBehaviour
{
    public TMP_Text userNameText;
    public TMP_Text ageText;
    public TMP_Text currentWeightText;
    public TMP_Text startedWeightText;
    public TMP_Text heightText;
    public TMP_Text targetWeightText;
    public TMP_Text weeksText;

    public TMP_Text squatsButtonText;
    public TMP_Text jumpingJacksButtonText;
    public TMP_Text sitUpsButtonText;
    public TMP_Text pushUpsButtonText;
    public TMP_Text congratsText;

    public Button squatsButton;
    public Button jumpingJacksButton;
    public Button sitUpsButton;
    public Button pushUpsButton;

    private float currentWeight;
    private float currentCaloriesBurned;

    private int targetSquats;
    private int targetJumpingJacks;
    private int targetSitUps;
    private int targetPushUps;

    private int currentSquats;
    private int currentJumpingJacks;
    private int currentSitUps;
    private int currentPushUps;

    void Start()
    {
        LoadUserData();
        LoadExerciseData();
        UpdateButtonTexts();
        UpdateCurrentWeight();
    }

    private void LoadUserData()
    {
        string userName = PlayerPrefs.GetString("UserName", "User");
        int age = PlayerPrefs.GetInt("Age", 0);
        int startedWeight = PlayerPrefs.GetInt("Weight", 0);
        int height = PlayerPrefs.GetInt("Height", 0);
        int targetWeight = PlayerPrefs.GetInt("TargetWeight", 0);
        int weeks = PlayerPrefs.GetInt("Weeks", 0);

        currentCaloriesBurned = PlayerPrefs.GetFloat("CaloriesBurned", 0f);
        currentWeight = startedWeight; // Kilo bilgisi başlangıç kilosuna ayarlandı

        if (userNameText != null)
            userNameText.text = userName;
        if (ageText != null)
            ageText.text = age.ToString();
        if (currentWeightText != null)
            currentWeightText.text = Mathf.Max(currentWeight, 0).ToString("F2");
        if (startedWeightText != null)
            startedWeightText.text = startedWeight.ToString();
        if (heightText != null)
            heightText.text = height.ToString();
        if (targetWeightText != null)
            targetWeightText.text = targetWeight.ToString();
        if (weeksText != null)
            weeksText.text = weeks.ToString();
    }

    private void UpdateCurrentWeight()
    {
        int startedWeight = PlayerPrefs.GetInt("Weight", 0);
        currentWeight = startedWeight - (currentCaloriesBurned / 1000f);

        if (currentWeightText != null)
        {
            currentWeightText.text = Mathf.Max(currentWeight, 0).ToString("F2");
        }
    }

    private void LoadExerciseData()
    {
        targetSquats = PlayerPrefs.GetInt("TargetSquats", 0);
        targetJumpingJacks = PlayerPrefs.GetInt("TargetJumpingJacks", 0);
        targetSitUps = PlayerPrefs.GetInt("TargetSitUps", 0);
        targetPushUps = PlayerPrefs.GetInt("TargetPushUps", 0);

        currentSquats = PlayerPrefs.GetInt("Squats", 0);
        currentJumpingJacks = PlayerPrefs.GetInt("JumpingJacks", 0);
        currentSitUps = PlayerPrefs.GetInt("SitUps", 0);
        currentPushUps = PlayerPrefs.GetInt("PushUps", 0);
    }

    private void UpdateButtonTexts()
    {
        squatsButtonText.text = $"Squats: {currentSquats}/{targetSquats}";
        jumpingJacksButtonText.text = $"Jumping Jacks: {currentJumpingJacks}/{targetJumpingJacks}";
        sitUpsButtonText.text = $"Sit-Ups: {currentSitUps}/{targetSitUps}";
        pushUpsButtonText.text = $"Push-Ups: {currentPushUps}/{targetPushUps}";

        squatsButton.interactable = currentSquats < targetSquats;
        jumpingJacksButton.interactable = currentJumpingJacks < targetJumpingJacks;
        sitUpsButton.interactable = currentSitUps < targetSitUps;
        pushUpsButton.interactable = currentPushUps < targetPushUps;

        if (currentSquats >= targetSquats && currentJumpingJacks >= targetJumpingJacks &&
            currentSitUps >= targetSitUps && currentPushUps >= targetPushUps)
        {
            congratsText.gameObject.SetActive(true);
        }
        else
        {
            congratsText.gameObject.SetActive(false);
        }
    }
}