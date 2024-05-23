using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance

    public int score;
    public Transform playerTransform;  // Reference to the Player's Transform
    public float growthFactor = 0.1f;  // Growth factor for the player

    private int lastScoreUpdate = 0;   // Score at the last size change

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure that there's only one GameManager instance
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optionally keep this object across scenes
        }
    }

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }

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