using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float maxDistanceSpeed = 0.1f; //distacia maxima q puede moverse
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = transform.position.y;  
                Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                
                //Limitar la distancia q puede moverse en el frame

                float distanceToMove = Vector3.Distance(transform.position, newPosition); //calcular la dsitancia q se moverá

                //si la distancia es mayor q maxDistanceSpeed, aplica el codigo de maxds
                if (distanceToMove > maxDistanceSpeed) 
                {
                    newPosition = transform.position + (newPosition - transform.position).normalized * maxDistanceSpeed;
                }

                transform.position = newPosition;
            }
        }
    }
}