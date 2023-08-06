using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Game variables.
    [SerializeField] private bool gameOver;
    [SerializeField] private int score;
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    // UI variables.
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public GameObject pauseButton;
    public GameObject retryPanel;
    // Spaceship limits.
    public GameObject spaceship;
    public readonly float xBoundaries = 14.0f;
    public readonly float groundLevel = 1.0f;
    // City building ground level spawn points and height.
    public GameObject[] buildingSegment;
    public GameObject[] buildingSegmentBase;
    private const int buildingColumns = 10;
    private const int buildingRowMin = 6;
    private const int buildingRowMax = 10;
    private readonly Vector3[] buildingSpawnPoints = new Vector3[buildingColumns]
    {
        new Vector3(-11.25f, 0.5f, -1.5f), new Vector3(-8.75f, 0.5f, -1.5f),
        new Vector3(-6.25f, 0.5f, -1.5f), new Vector3(-3.75f, 0.5f, -1.5f),
        new Vector3(-1.25f, 0.5f, -1.5f), new Vector3(1.25f, 0.5f, -1.5f),
        new Vector3(3.75f, 0.5f, -1.5f), new Vector3(6.25f, 0.5f, -1.5f),
        new Vector3(8.75f, 0.5f, -1.5f), new Vector3(11.25f, 0.5f, -1.5f)
    };

    // Spawn in a building at each ground spawnpoint.
    private void Start()
    {
        foreach (Vector3 spawnPoint in buildingSpawnPoints)
        {
            // Generate a random building height.
            int buildingHeight = Random.Range(buildingRowMin, buildingRowMax + 1);
            int buildingIndex = Random.Range(0, buildingSegment.Length);
            for (int i = 0; i < buildingHeight; ++i)
            {
                // Spawn the default prefab at every level.
                if (i > 0)
                {
                    Instantiate(buildingSegment[buildingIndex],
                        new Vector3(spawnPoint.x, spawnPoint.y + i, spawnPoint.z),
                        buildingSegment[buildingIndex].transform.rotation);
                }
                // Spawn the base prefab at the bottom.
                else
                {
                    Instantiate(buildingSegmentBase[buildingIndex],
                        new Vector3(spawnPoint.x, spawnPoint.y + i, spawnPoint.z),
                        buildingSegmentBase[buildingIndex].transform.rotation);
                }
            }
        }
        UpdateScore();
    }

    // Determine if the player won of lost by checking if the spaceship is intact.
    public void CheckIfVictory()
    {
        pauseButton.gameObject.SetActive(false);
        retryPanel.gameObject.SetActive(true);
        // If player has made a landing, display victory.
        if (spaceship.activeInHierarchy)
        {
            winText.gameObject.SetActive(true);
            score += 100;
            UpdateScore();
        }
        // Otherwise display defeat.
        else
        {
            loseText.gameObject.SetActive(true);
        }
        MenuManager.instance.playerScore = score;
    }

    // Display the score of the player for destroying building segments.
    public void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }
}
