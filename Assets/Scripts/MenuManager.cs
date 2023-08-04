using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Singleton to transfer information between scenes.
    public static MenuManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /*
    public string playerName;
    public string highestScoringPlayer;
    public int highestScore;

    [System.Serializable]
    class SaveData
    {
        public string highestScoringPlayer;
        public int highestScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new();
        data.highestScoringPlayer = highestScoringPlayer;
        data.highestScore = highestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highestScoringPlayer = data.highestScoringPlayer;
            highestScore = data.highestScore;
        }
    }

    public void ResetHighScore()
    {
        highestScoringPlayer = "";
        highestScore = 0;
        SaveHighScore();
    }
    */
}
