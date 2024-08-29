using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class MiniGameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text caloriesBurnedText;
    public TMP_Text countdownText;
    public LeaderboardManager leaderboardManager;
    public GameObject leaderboardPanel;

    private float timer = 30.0f; // Oyun süresi 30 saniye
    private bool gameActive = false;

    private float caloriesBurned;
    private const float caloriesPerIncrement = 5f;

    void Start()
    {
        // Kullanıcı verilerini yükle
        UserData.LoadUserData();
        ResetScore();
        caloriesBurned = PlayerPrefs.GetFloat("CaloriesBurned", 0f); // Kalori bilgisi yükleniyor
        UpdateScoreText();
        UpdateCaloriesText();
        StartCoroutine(CountdownAndStartGame());
    }

    void Update()
    {
        if (gameActive)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimerText();

                // Oyuncunun hareketini izlemek için (örneğin, bir tuş tıklamasıyla)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    IncrementScore();
                }
            }
            else
            {
                EndGame(); // Oyun sona erdiğinde
            }
        }
    }

    private void IncrementScore()
    {
        UserData.MiniGameCurrentScore++;
        UpdateScoreText();
        AddCalories(caloriesPerIncrement); // Kalori artırma
    }

    private void ResetScore()
    {
        UserData.MiniGameCurrentScore = 0;
        Debug.Log("Score Reset"); // Debug mesajı
    }

    private void UpdateScoreText()
    {
        scoreText.text = UserData.MiniGameCurrentScore.ToString();
    }

    private void UpdateTimerText()
    {
        timerText.text = "00:" + Mathf.Ceil(timer).ToString("00");
    }

    private void AddCalories(float calories)
    {
        caloriesBurned += calories;
        PlayerPrefs.SetFloat("CaloriesBurned", caloriesBurned);
        PlayerPrefs.Save();
        UpdateCaloriesText(); // Kalori güncellemesi
    }

    private void UpdateCaloriesText()
    {
        if (caloriesBurnedText != null)
        {
            caloriesBurnedText.text = caloriesBurned.ToString("F2");
        }
    }

    private void EndGame()
    {
        gameActive = false;

        // Skorun yüksek skoru geçip geçmediğini kontrol et
        if (UserData.MiniGameCurrentScore > UserData.MiniGameHighScore)
        {
            UserData.MiniGameHighScore = UserData.MiniGameCurrentScore;
            Debug.Log("EndGame - HighScore saved: " + UserData.MiniGameHighScore);
        }

        UserData.SaveUserData();

        // Leaderboard'u güncelle
        leaderboardManager.UpdateLeaderboard();
        leaderboardPanel.SetActive(true); // Leaderboard panelini göster
    }

    private IEnumerator CountdownAndStartGame()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        countdownText.text = "GO!";
        gameActive = true;
        ResetScore();
        UpdateScoreText();
        yield return new WaitForSeconds(1.0f);

        countdownText.gameObject.SetActive(false);
        UpdateTimerText();
    }
}