using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour
{
    public float sizeThreshold = 1.0f;
    public float proximityThreshold = 1.5f;
    private Collider playerCollider;

    private void Start()
    {
        playerCollider = GetComponent<Collider>();
        if (!playerCollider)
        {
            Debug.LogError("No Collider attached to the player!");
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            Debug.LogWarning("No Rigidbody attached to the player. Adding one.");
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // Set to kinematic if the player should not be affected by physics forces
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered with: " + other.name);
        if (CanAbsorb(other))
        {
            Debug.Log("Can absorb");
            if (IsCloseEnoughToCenter(other.transform))
            {
                Debug.Log("Is close enough to center");
                AbsorbObject(other.gameObject);
            }
        }
    }

    private bool CanAbsorb(Collider other)
    {
        return other.bounds.size.magnitude < playerCollider.bounds.size.magnitude * sizeThreshold;
    }

    private bool IsCloseEnoughToCenter(Transform otherTransform)
{
    // Encuentra el punto más cercano en el collider del jugador al objeto
    Vector3 closestPointOnPlayer = playerCollider.ClosestPoint(otherTransform.position);
    // Calcula la distancia desde este punto al centro del otro objeto
    float distanceToOtherCenter = Vector3.Distance(closestPointOnPlayer, otherTransform.position);
    // Obtiene el tamaño del collider del otro objeto para ajustar el umbral
    float sizeOfOther = otherTransform.GetComponent<Collider>().bounds.extents.magnitude;
    // Define un factor de invasión, por ejemplo, el objeto necesita estar dentro al menos la mitad de su tamaño
    float invasionFactor = sizeOfOther * 0.5f;
     Debug.Log($"Distance to center: {distanceToOtherCenter}, Invasion Factor: {invasionFactor}");

    // Comprueba si el objeto ha invadido el espacio del jugador más allá del factor de invasión definido
    return distanceToOtherCenter < invasionFactor;
}

    private void AbsorbObject(GameObject target)
    {
        Destroy(target);
    }
}