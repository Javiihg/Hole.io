using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float mapWidth = 20f;
    [SerializeField] private float mapHeight = 20f;
    [SerializeField] private float closeEnoughDistance = 0.5f; //Distancia rescpecto al target

    public int points;

    private Vector3 targetPosition;
    private Transform enemyTransform;

    void Start()
    {
        enemyTransform = transform;
        SetRandomTargetPosition();
    }

    void Update()
    {
        MoveTowardsTarget();
        CheckIfReachedTarget();
    }

    private void SetRandomTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), 0.1f, Random.Range(-mapHeight / 2, mapHeight / 2));
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void CheckIfReachedTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition) < closeEnoughDistance)
        {
            SetRandomTargetPosition();
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        points += pointsToAdd;
        Grow();
    }

    void Grow()
    {
        // Incrementa la escala cada vez que se agregan puntos
        float growthFactor = 0.05f; // Define cuánto crece
        transform.localScale += new Vector3(growthFactor, growthFactor, growthFactor);
    }
}