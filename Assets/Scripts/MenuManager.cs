using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Singleton to transfer information between scenes.
    public static readonly int highscoreListSize = 15;
    public static MenuManager instance;
    public string[] highestScoringPlayers = new string[highscoreListSize];
    public int[] highestScores = new int[highscoreListSize];
    public string playerName; // Empty inputs will just put three dashes.
    public int playerScore;
    [System.Serializable]
    class SaveData
    {
        public string[] highestScoringPlayers = new string[highscoreListSize];
        public int[] highestScores = new int[highscoreListSize];
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScores();
    }

    // Save the highscores list.
    public void SaveHighScores()
    {
        SaveData data = new();
        data.highestScoringPlayers = highestScoringPlayers;
        data.highestScores = highestScores;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    // Load the highscores list.
    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highestScoringPlayers = data.highestScoringPlayers;
            highestScores = data.highestScores;
        }
    }

    // Update the highscore if player has a higher score.
    public void UpdateHighscore()
    {
        // Empty names will just be three empty dashes.
        if (playerName == "") playerName = "---";
        // Variables to hold the scores temporarily.
        string tempName = playerName;
        int tempScore = playerScore;
        // Loop through the list in order to update the scoreboard.
        for (int i = 0; i < highscoreListSize; i++)
        {
            // If the highscore is higher than one on the scoreboard, place it and shift down.
            if (tempScore > highestScores[i])
            {
                // Temporary holder.
                int ts = highestScores[i];
                string tn = highestScoringPlayers[i];
                // Overwrite the score slot.
                highestScores[i] = tempScore;
                highestScoringPlayers[i] = tempName;
                // Write the former high score onto the temp slot and continue to iterate downwards.
                tempScore = ts;
                tempName = tn;
            }
        }
        // Save the updated list afterwards.
        SaveHighScores();
    }
}
