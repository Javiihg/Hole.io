using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour
{
    public float sizeThreshold = 1.0f;
    public float proximityThreshold = 1.5f;
    private Collider playerCollider;
    private LTDescr absorptionTween; 

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

        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            building.StartAbsorption();  // Inicia la absorción cuando entra en contacto
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            building.ResetAbsorption();  // Restablece la absorción si el edificio sale del rango
        }
    }

    private bool CanAbsorb(Collider other)
    {
        return other.bounds.size.magnitude < playerCollider.bounds.size.magnitude * sizeThreshold;
    }

    private bool IsCloseEnoughToCenter(Transform otherTransform)
    {
        Vector3 closestPointOnPlayer = playerCollider.ClosestPoint(otherTransform.position);
        float distanceToOtherCenter = Vector3.Distance(closestPointOnPlayer, otherTransform.position);
        float sizeOfOther = otherTransform.GetComponent<Collider>().bounds.extents.magnitude;
        float invasionFactor = sizeOfOther * 0.5f;
        Debug.Log($"Distance to center: {distanceToOtherCenter}, Invasion Factor: {invasionFactor}");

        return distanceToOtherCenter < invasionFactor;
    }

    private void AbsorbObject(GameObject target)
    {
        Destroy(target);
    }
}