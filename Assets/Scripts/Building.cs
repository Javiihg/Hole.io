using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private GameObject absorber; 
    public float absorptionTime = 1.0f;
    public int points = 250;
    public int cost = 100;

    private GameManager gameManager;
    private bool isBeingAbsorbed = false;
    private float currentAbsorption = 0f;
    private LTDescr absorptionTween;
    private Vector3 originalScale;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
            return;
        }

        originalScale = transform.localScale;
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

    public void StartAbsorption(GameObject absorber)
{
    if (!isBeingAbsorbed)
    {
        this.absorber = absorber; // Guarda el objeto que está absorbiendo
        if (absorber.tag == "Player" && GameManager.Instance.score >= cost)
        {
            isBeingAbsorbed = true;
            currentAbsorption = 0f;
            absorptionTween = LeanTween.scale(gameObject, Vector3.zero, absorptionTime).setOnComplete(Absorb);
        }
        else if (absorber.tag == "Enemy")
        {
            isBeingAbsorbed = true;
            currentAbsorption = 0f;
            absorptionTween = LeanTween.scale(gameObject, Vector3.zero, absorptionTime).setOnComplete(Absorb);
        }
    }
}

private void Absorb()
{
    if (isBeingAbsorbed)
    {
        if (GameManager.Instance != null && absorber.tag == "Player") // Corrige esta línea para verificar el objeto que absorbe
        {
            GameManager.Instance.AddPoints(points);
        }
        Destroy(gameObject);
        isBeingAbsorbed = false;
    }
}

    public void ResetAbsorption()
    {
        if (isBeingAbsorbed)
        {
            isBeingAbsorbed = false;
            currentAbsorption = 0f;
            LeanTween.cancel(absorptionTween.uniqueId);
            transform.localScale = originalScale;
        }
    }
}