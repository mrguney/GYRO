using UnityEngine;

public static class UserData
{
    public static string UserName;
    public static int Age;
    public static int Weight;
    public static int Height;
    public static int TargetWeight;
    public static int FootSteps;
    public static int Weeks;
    public static int Gender;
    public static float BMI;
    public static int Squats;
    public static int JumpingJacks;
    public static int SitUps;
    public static int PushUps;

    // MiniGame ile ilgili alanlar
    public static int MiniGameHighScore = 0;
    public static int MiniGameCurrentScore = 0;

    public static void SaveUserData()
    {
        PlayerPrefs.SetString("UserName", UserName);
        PlayerPrefs.SetInt("Age", Age);
        PlayerPrefs.SetInt("Weight", Weight);
        PlayerPrefs.SetInt("Height", Height);
        PlayerPrefs.SetInt("TargetWeight", TargetWeight);
        PlayerPrefs.SetInt("FootSteps", FootSteps);
        PlayerPrefs.SetInt("Weeks", Weeks);
        PlayerPrefs.SetInt("Gender", Gender);
        PlayerPrefs.SetFloat("BMI", BMI);
        PlayerPrefs.SetInt("Squats", Squats);
        PlayerPrefs.SetInt("JumpingJacks", JumpingJacks);
        PlayerPrefs.SetInt("SitUps", SitUps);
        PlayerPrefs.SetInt("PushUps", PushUps);

        // MiniGame verilerini kaydet
        PlayerPrefs.SetInt("MiniGameHighScore", MiniGameHighScore);
        PlayerPrefs.SetInt("MiniGameCurrentScore", MiniGameCurrentScore);
    }

    public static void LoadUserData()
    {
        UserName = PlayerPrefs.GetString("UserName", "User");
        Age = PlayerPrefs.GetInt("Age", 0);
        Weight = PlayerPrefs.GetInt("Weight", 0);
        Height = PlayerPrefs.GetInt("Height", 0);
        TargetWeight = PlayerPrefs.GetInt("TargetWeight", 0);
        FootSteps = PlayerPrefs.GetInt("FootSteps", 0);
        Weeks = PlayerPrefs.GetInt("Weeks", 0);
        Gender = PlayerPrefs.GetInt("Gender", 0);
        BMI = PlayerPrefs.GetFloat("BMI", 0f);
        Squats = PlayerPrefs.GetInt("Squats", 0);
        JumpingJacks = PlayerPrefs.GetInt("JumpingJacks", 0);
        SitUps = PlayerPrefs.GetInt("SitUps", 0);
        PushUps = PlayerPrefs.GetInt("PushUps", 0);

        // MiniGame verilerini yükle
        MiniGameHighScore = PlayerPrefs.GetInt("MiniGameHighScore", 0);
        MiniGameCurrentScore = PlayerPrefs.GetInt("MiniGameCurrentScore", 0);
    }
}