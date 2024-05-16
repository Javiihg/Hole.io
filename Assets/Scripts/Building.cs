using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float absorptionTime = 1.0f;
    public int poitns = 250;
    public bool isBeingAbsorbed = false;
    public float currentAbsorption = 0f;

    void Update()
    {
        if (isBeingAbsorbed)
        {
            currentAbsorption += absorptionTime.Time.delta;
            if (currentAbsorption >= absorptionTime)
            {
                isBeingAbsorbed();
            }
        }
    }

    void Absorb()
    {
         FindObjectOfType<GameManager>().AddPoints(points);
         Destroy(gameObject);
    }

    public void StartAbsorption()
    {
        isBeingAbsorbed = true;
    }

    public void ResetAbsorption()
    {
        isBeingAbsorbed = false;
        currentAbsorption = 0f;
    }
}
