using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float absorptionTime = 1.0f;  // Tiempo necesario para absorber completamente
    public int points = 250;             // Puntos otorgados al ser absorbido
    private GameManager gameManager;

    private bool isBeingAbsorbed = false;  // Indica si el edificio está siendo absorbido
    private float currentAbsorption = 0f;  // Tiempo que ha estado siendo absorbido
    private LTDescr absorptionTween;       // Referencia a la animación de LeanTween

    private Vector3 originalScale; // To store the original scale of the building

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        originalScale = transform.localScale; // Store the original scale at the start
    }

    void Update()
    {
        if (isBeingAbsorbed && currentAbsorption < absorptionTime)
        {
            currentAbsorption += Time.deltaTime;
            if (currentAbsorption >= absorptionTime)
            {
                Absorb();
            }
        }
    }

    void Absorb()
    {
        if (isBeingAbsorbed) // Only absorb if it's still being absorbed
        {
            if (gameManager != null)
            {
                gameManager.AddPoints(points);
            }
            else
            {
                Debug.LogError("GameManager is null.");
            }
            Destroy(gameObject);
        }
    }

    public void StartAbsorption()
    {
        if (!isBeingAbsorbed)
        {
            isBeingAbsorbed = true;
            currentAbsorption = 0f; // Ensure absorption starts from zero
            absorptionTween = LeanTween.scale(gameObject, Vector3.zero, absorptionTime).setOnComplete(Absorb);
        }
    }

    public void ResetAbsorption()
    {
        if (isBeingAbsorbed)
        {
            isBeingAbsorbed = false;
            currentAbsorption = 0f; // Reset the absorption progress
            if (absorptionTween != null) // Check if there is an active tween
            {
                LeanTween.cancel(absorptionTween.uniqueId);
            }
            transform.localScale = originalScale;  // Reset to original scale
        }
    }
}