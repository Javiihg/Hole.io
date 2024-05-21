using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public Transform playerTransform;  // Referencia al Transform del jugador
    public float growthFactor = 0.1f;  // Factor de crecimiento del jugador

    private int lastScoreUpdate = 0;   // Puntuación al último cambio de tamaño

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

    void UpdatePlayerSize()
    {
        // Comprobar si el puntaje ha pasado un múltiplo de 200 desde el último ajuste de tamaño
        if ((score - lastScoreUpdate) >= 200)
        {
            // Aumentar tamaño del jugador
            playerTransform.localScale *= (1 + growthFactor);

            // Actualizar lastScoreUpdate al último múltiplo de 200 alcanzado
            lastScoreUpdate += ((score - lastScoreUpdate) / 200) * 200;
        }
    }
}