using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    public Vector3 cityCenter;
    public int widthBuildings = 20;
    public int heightBuildings = 20;
    public float spaceBetweenElements = 2.0f;
    public GameObject[] buildingPrefabs;
    public GameObject roadPrefab;
    public GameObject[] stuffPrefabs;
    public float possibilityNotBuilding = 0.4f;
    public GameObject player;

    void Start()
    {
        GenerateCity();
        PlacePlayer();
    }

    void GenerateCity()
{
    // Calcular el punto de inicio basado en el centro de la ciudad
    Vector3 startPosition = new Vector3(
        cityCenter.x - (widthBuildings * spaceBetweenElements) / 2 + spaceBetweenElements / 2,
        cityCenter.y,
        cityCenter.z - (heightBuildings * spaceBetweenElements) / 2 + spaceBetweenElements / 2
    );

    Vector3 position = startPosition;

    for (int x = 0; x < widthBuildings; x++)
    {
        for (int y = 0; y < heightBuildings; y++)
        {
            GameObject toInstantiate;
            if (x % 2 == 0) // Si x es par, decide si colocar un edificio o stuff
            {
                if (Random.value > possibilityNotBuilding && buildingPrefabs.Length > 0)
                {
                    toInstantiate = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
                }
                else if (stuffPrefabs.Length > 0)
                {
                    toInstantiate = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
                }
                else
                {
                    continue;
                }
            }
            else // Si x es impar, colocar una carretera
            {
                toInstantiate = roadPrefab;
            }

            Instantiate(toInstantiate, position, Quaternion.identity);
            position.z += spaceBetweenElements;
        }
        position.z = startPosition.z; // Resetear z a la posición de inicio de la fila
        position.x += spaceBetweenElements;
    }
}

    void PlacePlayer()
{
    if (player != null)
    {
        // Calcular la posición de la esquina inferior izquierda
        // Nota: Asumimos que cityCenter está realmente en el centro del mapa generado.
        Vector3 playerStartPosition = new Vector3(
            cityCenter.x - (widthBuildings * spaceBetweenElements) / 2,
            player.transform.position.y, // Mantén la altura original del jugador
            cityCenter.z - (heightBuildings * spaceBetweenElements) / 2
        );
        player.transform.position = playerStartPosition;
    }
    else
    {
        Debug.LogError("Player object is not assigned in the inspector!");
    }
}
}