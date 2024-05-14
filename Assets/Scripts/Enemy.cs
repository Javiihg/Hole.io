using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float namnamMultiplier = 2f;
    private Vector3 targetPosition;

    void Start()
    {
        SetRandomTargetPosition();
    }

    void Update()
    {
        MoveTowardsTarget();
        CheckIfReachedTarget();
    }

    void SetRandomTargetPosition()
    {
        float mapWidth = 20f;
        float mapHeigh = 20f;
        targetPosition = new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), 0.1f, Random.Range(-mapHeigh / 2, mapHeigh / 2));
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void CheckIfReachedTarget()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            SetRandomTargetPosition();
        }
    }
}
