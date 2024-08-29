using UnityEngine;
using TMPro;
using System;

public class GyroAnimationController : MonoBehaviour
{
    private Animator[] animators;

    private float minTilt = -1.0f;
    private float maxTilt = 1.0f;
    private float smoothTilt;
    private float smoothFactor = 0.1f;
    private float tiltThreshold = 0.1f;

    private bool reachedStart;
    private bool reachedEnd;
    private bool incrementedScore;

    public string exerciseType;
    public TMP_Text scoreText;
    public TMP_Text caloriesText;

    private float totalCalories;

    // Her egzersiz için kalori değerleri
    private int caloriesPerSquat = 5;
    private int caloriesPerJumpingJack = 8;
    private int caloriesPerSitUp = 6;
    private int caloriesPerPushUp = 9; 
    private int caloriesPerMini = 5;

    void Start()
    {
        animators = FindObjectsOfType<Animator>();

        foreach (Animator anim in animators)
        {
            anim.speed = 0;
        }

        totalCalories = PlayerPrefs.GetFloat("CaloriesBurned", 0f); // Burada `CaloriesBurned` olarak güncelledim
        UpdateCaloriesText();
        ResetFlags();
    }

    void Update()
    {
        float tilt = Input.acceleration.y;

        smoothTilt = Mathf.Lerp(smoothTilt, tilt, smoothFactor);

        float tiltNormalized = Mathf.InverseLerp(minTilt, maxTilt, smoothTilt);

        if (Mathf.Abs(smoothTilt) > tiltThreshold)
        {
            foreach (Animator anim in animators)
            {
                anim.Play(exerciseType, 0, tiltNormalized);
                anim.speed = 0;
            }

            if (tiltNormalized <= 0.2f && !reachedStart)
            {
                reachedStart = true;
                Debug.Log("Reached Start");
            }

            if (tiltNormalized >= 0.8f && !reachedEnd)
            {
                reachedEnd = true;
                Debug.Log("Reached End");
            }

            if (reachedStart && reachedEnd && !incrementedScore)
            {
                IncrementScore();
                IncrementCalories();
                Debug.Log("Score Incremented");
                incrementedScore = true;
            }
        }
        else
        {
            if (incrementedScore)
            {
                ResetFlags();
                Debug.Log("Flags Reset");
            }
        }
    }

    private void IncrementScore()
    {
        int currentScore = PlayerPrefs.GetInt(exerciseType, 0);
        currentScore++;
        PlayerPrefs.SetInt(exerciseType, currentScore);
        PlayerPrefs.Save();
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    private void IncrementCalories()
    {
        switch (exerciseType)
        {
            case "Squats":
                totalCalories += caloriesPerSquat;
                break;
            case "JumpingJacks":
                totalCalories += caloriesPerJumpingJack;
                break;
            case "SitUps":
                totalCalories += caloriesPerSitUp;
                break;
            case "PushUps":
                totalCalories += caloriesPerPushUp;
                break;
            case "MinigameSquats":
                totalCalories += caloriesPerMini;
                break;
            default:
                Debug.LogWarning("Unknown exercise type: " + exerciseType);
                break;
        }

        PlayerPrefs.SetFloat("CaloriesBurned", totalCalories); // Güncel olarak `CaloriesBurned` ile kayıt yapıyoruz
        PlayerPrefs.Save();
        UpdateCaloriesText();
    }

    private void UpdateCaloriesText()
    {
        if (caloriesText != null)
        {
            caloriesText.text = totalCalories.ToString("F2");
        }
    }

    private void ResetFlags()
    {
        reachedStart = false;
        reachedEnd = false;
        incrementedScore = false;
    }
}