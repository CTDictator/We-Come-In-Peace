using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject pauseButton;

    // Play the game.
    public void StartNewGame()
    {
        //MenuManager.instance.playerName = playerName;
        SceneManager.LoadScene(1);
    }

    // Quit.
    public void QuitGame()
    {
        //MenuManager.instance.SaveHighScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    // Return to the game menu.
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
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

    public void ViewHighscores()
    {
        SceneManager.LoadScene(2);
    }

    /*
    public Text highScoreText;
    public string playerName;

    private void Start()
    {
        DisplayHighScore();
    }

    public void GetPlayerName(string name)
    {
        playerName = name;
    }

    public void DisplayHighScore()
    {
        highScoreText.text = $"Best Score : {MenuManager.instance.highestScoringPlayer} :"
            + $" {MenuManager.instance.highestScore}";
    }

    public void ResetHighScore()
    {
        MenuManager.instance.ResetHighScore();
    }
    */
}
