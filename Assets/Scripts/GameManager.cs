using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI timerText;
    public float timeRemaining = 120;
    private bool timerRunning = false;

    public bool gameHasEnded = false;

    public GameObject exitButton;

    public GameObject restartButton;

    public TextMeshProUGUI maxScoreText;

    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI scoreText;
    public int score;
    public Transform playerTransform;
    public float growthFactor = 0.1f;

    private int lastScoreUpdate = 0;
    public GameObject defeatScreen;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartTimer();
        }
    }

    void Start()
    {
        scoreText.text = "Score: " + score;
        defeatScreen.SetActive(false);

        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        maxScoreText.text = "Max Score: " + maxScore;
    }

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                EndGame();
            }
        }
    }
    
    public void StartTimer()
    {
        timerRunning = true;
        timeRemaining = 120;
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score;
        Debug.Log("Total Points: " + score);
        UpdatePlayerSize();
    }

    private void UpdatePlayerSize()
    {
        if ((score - lastScoreUpdate) >= 400)
        {
            playerTransform.localScale *= (1 + growthFactor);
            lastScoreUpdate = score;
        }
    }

    public void RewardEnemies(int points)
{
    Enemy[] enemies = FindObjectsOfType<Enemy>();
    foreach (Enemy enemy in enemies)
    {
        if (enemy.gameObject.tag == "Enemy")
        {
            Debug.Log("Rewarding enemy: " + enemy.name);
            enemy.AddPoints(points);
        }
    }
}

    public void EndGame()
    {
        timerRunning = false;
        gameHasEnded = true;
        defeatScreen.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
        timerText.text = "00:00";
        Debug.Log("Game Over!");

        int maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", score);
            PlayerPrefs.Save();
            maxScoreText.text = "Max Score: " + score;
        }
        exitButton.SetActive(true);
    }

    public void RestartGame()
    {
        score = 0;
    timeRemaining = 120;
    timerRunning = true;
    gameHasEnded = false;
    scoreText.text = "Score: " + score;
    maxScoreText.text = "Max Score: " + PlayerPrefs.GetInt("MaxScore", 0);
    defeatScreen.SetActive(false);


    // Reinicia la visibilidad y estado de todos los botones necesarios.
    restartButton.SetActive(true);
    exitButton.SetActive(true);

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game.");
        Application.Quit();
    }
}