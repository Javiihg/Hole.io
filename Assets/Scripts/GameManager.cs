using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 

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
        }
    }

    /*void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }*/

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        Debug.Log("Total Points: " + score);
        UpdatePlayerSize();
    }

    private void UpdatePlayerSize()
    {
        if ((score - lastScoreUpdate) >= 200)
        {
            playerTransform.localScale *= (1 + growthFactor);
            lastScoreUpdate += ((score - lastScoreUpdate) / 200) * 200;
        }
    }
}