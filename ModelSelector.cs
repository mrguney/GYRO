using UnityEngine;

public class ModelSelector : MonoBehaviour
{
    public GameObject maleModel_55_60_100Plus;
    public GameObject femaleModel_30_40_55Plus;
    public GameObject maleModel_25_35_90Plus;
    public GameObject femaleModel_45_55_60Plus;

    void Start()
    {
        SelectModel();
    }

    void SelectModel()
    {
        int age = PlayerPrefs.GetInt("Age");
        int weight = PlayerPrefs.GetInt("Weight");
        int gender = PlayerPrefs.GetInt("Gender");

        Debug.Log($"Yaş: {age}, Kilo: {weight}, Cinsiyet: {gender}");

        // Tüm modelleri devre dışı bırak
        DeactivateAllModels();

        // Yaş, kilo ve cinsiyete göre uygun modeli seç ve etkinleştir
        if (gender == 1) // Erkek
        {
            if (age >= 55 && age <= 60 && weight >= 100)
            {
                maleModel_55_60_100Plus.SetActive(true);
            }
            else if (age >= 18 && age <= 54 && weight >= 90)
            {
                maleModel_25_35_90Plus.SetActive(true);
            }
        }
        else if (gender == 0) // Kadın
        {
            if (age >= 18 && age <= 40 && weight >= 50)
            {
                femaleModel_30_40_55Plus.SetActive(true);
            }
            else if (age >= 45 && age <= 55 && weight >= 60)
            {
                femaleModel_45_55_60Plus.SetActive(true);
            }
        }
    }

    void DeactivateAllModels()
    {
        maleModel_55_60_100Plus.SetActive(false);
        femaleModel_30_40_55Plus.SetActive(false);
        maleModel_25_35_90Plus.SetActive(false);
        femaleModel_45_55_60Plus.SetActive(false);
    }
}