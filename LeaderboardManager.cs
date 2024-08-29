using UnityEngine;
using TMPro;
using System;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardEntryPrefab; // Prefab referansı
    public Transform leaderboardContentParent; // Prefab'lerin yerleştirileceği parent object
    public TMP_Text playerScoreEntry;  // Oyuncu skoru için text alanı

    private string[] fakeNames = { "Alice", "Bob", "Charlie", "Dave", "Eve", "Frank", "Grace", "Heidi", "Ivan" };
    private int[] fakeScores = { 25, 30, 35, 40, 45, 28, 32, 38, 42 };

    private int playerScore = 0;
    private string playerName = "You";

    void Start()
    {
        playerScore = PlayerPrefs.GetInt("currentScore");
        playerName = PlayerPrefs.GetString("UserName", "You");

        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        int[] allScores = new int[10];
        string[] allNames = new string[10];

        for (int i = 0; i < 9; i++)
        {
            allScores[i] = fakeScores[i];
            allNames[i] = fakeNames[i];
        }

        allScores[9] = playerScore;
        allNames[9] = playerName;

        Array.Sort(allScores, allNames); // Skorları sırala
        Array.Reverse(allScores); // Sıralamayı tersine çevir
        Array.Reverse(allNames);  // İsimleri sıralamaya uygun olarak tersine çevir

        foreach (Transform child in leaderboardContentParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < allScores.Length; i++)
        {
            GameObject newEntry = Instantiate(leaderboardEntryPrefab, leaderboardContentParent);
            TMP_Text[] texts = newEntry.GetComponentsInChildren<TMP_Text>();

            TMP_Text rankText = texts[0];
            TMP_Text nameText = texts[1];
            TMP_Text scoreText = texts[2];

            rankText.text = "#" + (i + 1).ToString();  // Sıralama numarası
            nameText.text = allNames[i];         // Oyuncu ismi
            scoreText.text = allScores[i].ToString(); // Skor

            if (allNames[i] == playerName)
            {
                Debug.Log("Player Entry Updated - New High Score: " + playerScore);  // Debug için
                playerScoreEntry.text = $"New High Score: {playerScore}";
            }
            else
            {
                Debug.Log("Leaderboard Entry - Rank: " + (i + 1) + ", Name: " + allNames[i] + ", Score: " + allScores[i]);
            }
        }

        int playerFinalPosition = Array.IndexOf(allNames, playerName);

        if (playerFinalPosition >= 3)
        {
            playerScoreEntry.text = $"#{playerFinalPosition + 1} {playerName}: {playerScore}";
            playerScoreEntry.gameObject.SetActive(true);
        }
        else
        {
            playerScoreEntry.gameObject.SetActive(false);
        }
    }
}