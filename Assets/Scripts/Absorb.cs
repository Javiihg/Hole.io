using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour
{
     public float sizeThreshold = 1.0f;
    public float proximityThreshold = 1.5f;
    private Collider playerCollider;
    private Rigidbody rb;

    private void Start()
{
    playerCollider = GetComponent<Collider>();
    rb = GetComponent<Rigidbody>();

    if (playerCollider == null)
    {
        playerCollider = gameObject.AddComponent<BoxCollider>();  // Añade un BoxCollider si no se encuentra ninguno
    }

    if (rb == null)
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; 
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
    if (playerCollider == null) playerCollider = GetComponent<Collider>();  // Intentar obtener el Collider nuevamente
    if (other == null || playerCollider == null)
    {
        return false;
    }

    bool isSmaller = other.bounds.size.magnitude < playerCollider.bounds.size.magnitude * sizeThreshold;
    bool isCloseEnough = IsCloseEnoughToCenter(other.transform);
    return isSmaller && isCloseEnough;
}

    private bool IsCloseEnoughToCenter(Transform otherTransform)
{
    if (playerCollider == null || otherTransform == null || otherTransform.GetComponent<Collider>() == null)
    {

        return false;
    }

    Vector3 closestPoint = playerCollider.ClosestPoint(otherTransform.position);
    float distance = Vector3.Distance(closestPoint, otherTransform.position);
    float invasionThreshold = otherTransform.GetComponent<Collider>().bounds.extents.magnitude * 0.5f;
    return distance < invasionThreshold;
}
    private void AbsorbObject(GameObject target)
{
    if (target == null) return;

    float scaleDownTime = 1.0f;
    LeanTween.scale(target, Vector3.zero, scaleDownTime).setOnComplete(() => {
        if (target != null) Destroy(target);
    });
}

    private void HandleAbsorption(Collider other, bool start)  
    {
        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            if (start) building.StartAbsorption();
            else building.ResetAbsorption();
        }
    }
}