using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 

    public TextMeshProUGUI timerText;
    public float timeRemaining = 120;
    private bool timerRunning = false;

    public int score;
    public Transform playerTransform;  
    public float growthFactor = 0.1f;  

    private int lastScoreUpdate = 0;   

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

    /*void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }*/

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
                TimerEnded();
                timeRemaining = 0;
                timerRunning = false;
            }
        }
    }
    
    public void StartTimer()
    {
        timerRunning = true;
        timeRemaining = 120;
    }

    void UpdateTimerDisplay (float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimerEnded()
    {
        timerText.text = "00:00";
        Debug.Log("Se acabó!!");
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        Debug.Log("Total Points: " + score);
        UpdatePlayerSize();
    }

    private void UpdatePlayerSize()
    {
        if ((score - lastScoreUpdate) >= 400)
        {
            playerTransform.localScale *= (1 + growthFactor);
            lastScoreUpdate += ((score - lastScoreUpdate) / 200) * 200;
        }
    }
}