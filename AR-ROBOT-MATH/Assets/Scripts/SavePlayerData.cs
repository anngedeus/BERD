using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SavePlayerData : MonoBehaviour
{
    public static List<PlayerData> playerDataList = new List<PlayerData>();

    // used for saving the player data upon entry
    public static void Save()
{
    string filePath = Application.dataPath + "/Resources/Data/savedPlayerData.csv";
    List<PlayerData> updatedPlayerDataList = new List<PlayerData>();

    // Iterate through the existing playerDataList
    foreach (PlayerData existingPlayerData in playerDataList)
    {
        // Check if the current user name already exists in the updated list
        bool userExists = false;
        foreach (PlayerData updatedPlayerData in updatedPlayerDataList)
        {
            if (existingPlayerData.userName == updatedPlayerData.userName)
            {
                // Update the user's score only if the new score is higher
                if (existingPlayerData.userScore > updatedPlayerData.userScore)
                {
                    updatedPlayerData.userScore = existingPlayerData.userScore;
                }
                userExists = true;
                break;
            }
        }

        // If the user does not exist in the updated list, add them
        if (!userExists)
        {
            updatedPlayerDataList.Add(existingPlayerData);
        }
    }

    // Write the updated data to the file
    StreamWriter writer = new StreamWriter(filePath, false); // Open the file in overwrite mode
    foreach (PlayerData playerData in updatedPlayerDataList)
    {
        writer.WriteLine(playerData.userName + "," + playerData.userScore);
        Debug.Log("Player Name: " + playerData.userName + ", Score: " + playerData.userScore);
    }
    writer.Close();
}


    // used for leading the leaderboard at the end of the game
    public static void Load()
    {
        string filePath = Application.dataPath  + "/Resources/Data/savedPlayerData.csv";
        if (File.Exists(filePath))
        {
            playerDataList.Clear(); // Clear the list before loading
            StreamReader reader = new StreamReader(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    string userName = parts[0];
                    int userScore;
                    if (int.TryParse(parts[1], out userScore))
                    {
                        PlayerData playerData = new PlayerData(userName, userScore);
                        playerDataList.Add(playerData);
                    }
                }
            }
            reader.Close();
            Debug.Log("Loaded player data");
        }
        else
        {
            Debug.Log("No saved player data found");
        }
    }
}