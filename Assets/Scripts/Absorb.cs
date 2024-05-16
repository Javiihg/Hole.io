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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CanAbsorb(other) && IsCloseEnoughToCenter(other.transform))
        {
            AbsorbObject(other.gameObject);
        }
    }

    private bool CanAbsorb(Collider other)
    {
        return other.bounds.size.magnitude < playerCollider.bounds.size.magnitude * sizeThreshold;
    }

    private bool IsCloseEnoughToCenter(Transform otherTransform)
    {
        return Vector3.Distance(transform.position, otherTransform.position) < proximityThreshold;
    }

    private void AbsorbObject(GameObject target)
    {
        Destroy(target);
    }
}