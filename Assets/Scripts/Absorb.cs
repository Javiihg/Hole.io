using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorb : MonoBehaviour
{
    public float sizeThreshold = 1.0f;  // Determinar si puede ser absorbido
    private Collider playerCollider;
    private Rigidbody rb;
    private Dictionary<GameObject, Vector3> originalSizes = new Dictionary<GameObject, Vector3>();  //Almacenar las escalas originales

    public float growthFactor = 0.1f;
    public Transform playerTransform;

    private void Start()
    {
        playerCollider = GetComponent<Collider>() ?? gameObject.AddComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && ShouldAbsorb(other))
        {
            StartAbsorption(other.gameObject);
        }
        HandleAbsorption(other, true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other != null && ShouldAbsorb(other))
        {
            ContinueAbsorption(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            ResetAbsorption(other.gameObject);  // Resetear la absorción al salir del rango
        }
        HandleAbsorption(other, false);
    }

    private bool ShouldAbsorb(Collider other)
    {
        if (playerCollider == null)
        {
            return false;
        }

        //Compara tamaño de ambos jugadores para poder absorber
        bool isSmaller = other.bounds.size.magnitude < playerCollider.bounds.size.magnitude * sizeThreshold;
        Vector3 closestPoint = playerCollider.ClosestPoint(other.transform.position);
        float distanceToCenter = Vector3.Distance(closestPoint, other.transform.position);
        bool isCloseEnough = distanceToCenter < other.bounds.extents.magnitude;

        return isSmaller && isCloseEnough;
    }
    
    private void StartAbsorption(GameObject target)
{
    if (target == null) return;

    if (!originalSizes.ContainsKey(target))
    {
        originalSizes[target] = target.transform.localScale;
    }

    if ((gameObject.tag == "Player" && target.tag == "Enemy") || (gameObject.tag == "Enemy" && target.tag == "Player"))
    {
        LeanTween.scale(target, Vector3.zero, 0.5f).setOnComplete(() => {
            Destroy(target);
            originalSizes.Remove(target);
            GameManager.Instance.AddPoints(200);
            if (target.tag == "Player") 
            {
                GameManager.Instance.EndGame();
            }
        });
    }

    if ((gameObject.tag == "Player" && target.tag == "PowerUp"))
    {
        LeanTween.scale(target, Vector3.zero, 0.5f).setOnComplete(() => {
            Destroy(target);
            GameManager.Instance.AddTime(20);
        });
    }

    if ((gameObject.tag == "Player" && target.tag == "PowerLess"))
    {
        LeanTween.scale(target, Vector3.zero, 0.5f).setOnComplete(() => {
            Destroy(target);
            GameManager.Instance.AddPoints(-50);
            playerTransform.localScale *= (1 - growthFactor);
        });
    }
}

    private void ContinueAbsorption(GameObject target)
    {
        
    }

    private void ResetAbsorption(GameObject target)
    {
        if (target != null && originalSizes.ContainsKey(target))
        {
            LeanTween.cancel(target);  
            target.transform.localScale = originalSizes[target];  
            originalSizes.Remove(target);  
        }
    }

    private void HandleAbsorption(Collider other, bool start)
    {
        //Mantener las ciudades mientras se absorben
        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            if (start)
            {
                building.StartAbsorption(this.gameObject);
            }
            else
            {
                building.ResetAbsorption();
            }
        }
    }
}