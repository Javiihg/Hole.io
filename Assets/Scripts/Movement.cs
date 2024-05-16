using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxDistanceSpeed = 0.1f; // Máxima distancia que el jugador puede moverse por frame
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToPosition(Input.mousePosition);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                MoveToPosition(touch.position);
            }
        }
    }

    void MoveToPosition(Vector3 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y;  
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                
            // Limitar la distancia que se puede mover en el frame
            if (Vector3.Distance(transform.position, newPosition) > maxDistanceSpeed)
            {
                newPosition = transform.position + (newPosition - transform.position).normalized * maxDistanceSpeed;
            }

            transform.position = newPosition;
        }
    }
}