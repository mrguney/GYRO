using UnityEngine;
using TMPro;

public class DisplayUserData : MonoBehaviour
{
    public TMP_Text userNameText;
    public TMP_Text userDataText;
    public TMP_Text currentWeightText;
    public TMP_Text currentCaloriesText;

    void Start()
    {
        if (userNameText != null)
        {
            userNameText.text = $"Welcome, {PlayerPrefs.GetString("UserName", "User")}!";
        }
        else
        {
            Debug.LogError("userNameText is not assigned in the inspector.");
        }

        if (userDataText != null)
        {
            userDataText.text = $"Age: {PlayerPrefs.GetInt("Age", 0)}\n" +
                                $"Weight: {PlayerPrefs.GetInt("Weight", 0)}\n" +
                                $"Height: {PlayerPrefs.GetInt("Height", 0)}\n" +
                                $"Target Weight: {PlayerPrefs.GetInt("TargetWeight", 0)}\n" +
                                $"Weeks: {PlayerPrefs.GetInt("Weeks", 0)}";
        }
        else
        {
            Debug.LogError("userDataText is not assigned in the inspector.");
        }

        UpdateCurrentWeight();
        UpdateCurrentCalories();
    }

    void UpdateCurrentWeight()
    {
        if (currentWeightText != null)
        {
            currentWeightText.text = $"Current Weight: {PlayerPrefs.GetInt("Weight", 0)} kg";
        }
        else
        {
            Debug.LogError("currentWeightText is not assigned in the inspector.");
        }
    }

    void UpdateCurrentCalories()
    {
        if (currentCaloriesText != null)
        {
            currentCaloriesText.text = $"Calories Burned: {PlayerPrefs.GetFloat("CaloriesBurned", 0f):F2}";
        }
        else
        {
            Debug.LogError("currentCaloriesText is not assigned in the inspector.");
        }
    }
}