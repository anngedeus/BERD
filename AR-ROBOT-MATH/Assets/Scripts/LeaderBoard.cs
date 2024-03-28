using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    public TextMeshProUGUI[] playerNameTexts; // Array of UI elements to display player names
    public TextMeshProUGUI[] playerScoreTexts; // Array of UI elements to display player scores

    void Start()
    {
        // Load player data
        SavePlayerData.Load();

        // Populate leaderboard UI
        UpdateLeaderboardUI();
    }

    private void UpdateLeaderboardUI()
    {
        // Ensure that the UI arrays are of the same length as playerDataList
        if (SavePlayerData.playerDataList.Count != playerNameTexts.Length ||
            SavePlayerData.playerDataList.Count != playerScoreTexts.Length)
        {
            Debug.LogError("UI arrays and player data list are of different lengths.");
            return;
        }

        // Populate UI elements with player data
        for (int i = 0; i < SavePlayerData.playerDataList.Count; i++)
        {
            playerNameTexts[i].text = SavePlayerData.playerDataList[i].userName;
            playerScoreTexts[i].text = SavePlayerData.playerDataList[i].userScore.ToString();
        }
    }
}
