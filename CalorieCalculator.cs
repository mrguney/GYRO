using UnityEngine;
using TMPro;
using System;

public class CalorieCalculator : MonoBehaviour
{
    public TMP_Text calorieText; // Paneldeki kalori bilgisini gösterecek text
    public TMP_Text infoText; // Ek bilgi metni

    void Start()
    {
        // Kullanıcı bilgilerini yükle
        UserData.LoadUserData();

        // Kaloriyi hesapla
        int estimatedCalories = CalculateCalories();

        // Kalori bilgisini ve ek bilgiyi göster
        calorieText.text = $"Estimated Daily Caloric Needs: {estimatedCalories} kcal";
        infoText.text = "This calculation is based on an average exercise level.";
    }

    private int CalculateCalories()
    {
        // BMR hesaplama (örnek erkek için)
        float BMR = 0f;
        if (UserData.Gender == 1) // Erkek
        {
            BMR = 88.362f + (13.397f * UserData.Weight) + (4.799f * UserData.Height) - (5.677f * UserData.Age);
        }
        else // Kadın
        {
            BMR = 447.593f + (9.247f * UserData.Weight) + (3.098f * UserData.Height) - (4.330f * UserData.Age);
        }

        // TDEE hesaplama (Orta seviye aktivite için)
        int TDEE = Mathf.RoundToInt(BMR * 1.55f);

        return TDEE;
    }
}