using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject pauseButton;
    public TextMeshProUGUI highscoreList;
    [SerializeField] private string playerName;

    public void Start()
    {
        DisplayHighscores();
    }

    // Play the game.
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    // Quit.
    public void QuitGame()
    {
        MenuManager.instance.SaveHighScores();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Return to the game menu.
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Pause the game and display options.
    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    // Resume the game.
    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;
    }

    // Switch to the highscore scene.
    public void ViewHighscores()
    {
        // Use the players tag as their name, if any.
        MenuManager.instance.playerName = playerName;
        // Update the highscore board if applicable.
        MenuManager.instance.UpdateHighscore();
        SceneManager.LoadScene(2);
    }

    // Get the players name after game is over.
    public void GetPlayerName(string name)
    {
        playerName = name;
    }

    // Display all the highscores.
    public void DisplayHighscores()
    {
        if (highscoreList != null)
        {
            for (int i = 0; i < MenuManager.highscoreListSize; i++)
            {
                highscoreList.text += $"{MenuManager.instance.highestScoringPlayers[i]}\t\t" +
                    $"{MenuManager.instance.highestScores[i]}\n";
            }
        }
    }
}
