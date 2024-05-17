using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        Debug.Log("Total Points: " + score);
        UpdatePlayerSize();
    }

    void UpdatePlayerSize()
    {
        
    }
}
