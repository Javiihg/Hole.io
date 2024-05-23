using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour
{
    public float sizeThreshold = 1.0f;  // Threshold to determine if an object can be absorbed based on size
    private Collider playerCollider;
    private Rigidbody rb;

    private void Start()
    {
        playerCollider = GetComponent<Collider>();
        if (playerCollider == null)
        {
            playerCollider = gameObject.AddComponent<BoxCollider>();
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;  // Set Rigidbody to kinematic to not affect physics calculations
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ShouldAbsorb(other))
        {
            AbsorbObject(other.gameObject);
        }
        HandleAbsorption(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleAbsorption(other, false);
    }

    private bool ShouldAbsorb(Collider other)
{
    if (other == null || playerCollider == null)
    {
        return false;
    }

    // Comprobar si el otro objeto es más pequeño en tamaño.
    bool isSmaller = other.bounds.size.magnitude < playerCollider.bounds.size.magnitude * sizeThreshold;

    // Comprobar si el otro objeto está lo suficientemente cerca del centro del absorbedor.
    Vector3 closestPoint = playerCollider.ClosestPoint(other.transform.position);
    float distanceToCenter = Vector3.Distance(closestPoint, other.transform.position);
    bool isCloseEnough = distanceToCenter < other.bounds.extents.magnitude; // Modificar según necesidades específicas

    return isSmaller && isCloseEnough;
}

    
private void AbsorbObject(GameObject target)
{
    if (target == null) return;

    // Diferenciar entre jugador y enemigo.
    if (gameObject.tag == "Player" && target.tag == "Enemy" || gameObject.tag == "Enemy" && target.tag == "Player")
    {
        // Realizar la absorción
        float scaleDownTime = 0.5f; // Puede ser más rápido para los enemigos si se desea
        LeanTween.scale(target, Vector3.zero, scaleDownTime).setOnComplete(() => {
            Destroy(target);  // Destruir el objeto absorbido
        });
    }
}

    private void HandleAbsorption(Collider other, bool start)
    {
        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            if (start) building.StartAbsorption(this.gameObject);  // Start absorption when entering the trigger
            else building.ResetAbsorption();  // Reset absorption when exiting the trigger
        }
    }
}