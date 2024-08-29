using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarGraphManager : MonoBehaviour
{
    public RectTransform dailyCalorieBar;
    public RectTransform targetCalorieBar;
    public TMP_Text dailyCalorieText;
    public TMP_Text targetCalorieText;

    private int dailyCalories;
    private int targetCalories;

    void Start()
    {
        // Load user data and calculate calories
        UserData.LoadUserData();
        dailyCalories = CalculateDailyCalories();
        targetCalories = Mathf.RoundToInt(dailyCalories * 0.8f);

        // Set bar heights based on calculated values
        SetBarHeights();

        // Display calorie values on the bars
        dailyCalorieText.text = $"{dailyCalories} kcal";
        targetCalorieText.text = $"{targetCalories} kcal";
    }

    private int CalculateDailyCalories()
    {
        // Basal Metabolic Rate (BMR) calculation
        float BMR = 0f;
        if (UserData.Gender == 1) // Male
        {
            BMR = 88.362f + (13.397f * UserData.Weight) + (4.799f * UserData.Height) - (5.677f * UserData.Age);
        }
        else // Female
        {
            BMR = 447.593f + (9.247f * UserData.Weight) + (3.098f * UserData.Height) - (4.330f * UserData.Age);
        }

        // Total Daily Energy Expenditure (TDEE) calculation based on moderate activity level
        int TDEE = Mathf.RoundToInt(BMR * 1.55f);

        return TDEE;
    }

    private void SetBarHeights()
    {
        float maxHeight = 200f; // Maximum height for the bars in pixels

        float dailyCalorieHeight = (dailyCalories / 3000f) * maxHeight; // Assuming 3000 kcal as a high baseline
        float targetCalorieHeight = (targetCalories / 3000f) * maxHeight;

        dailyCalorieBar.sizeDelta = new UnityEngine.Vector2(dailyCalorieBar.sizeDelta.x, dailyCalorieHeight);
        targetCalorieBar.sizeDelta = new UnityEngine.Vector2(targetCalorieBar.sizeDelta.x, targetCalorieHeight);
    }
}