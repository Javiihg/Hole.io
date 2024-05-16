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
    public GameObject[] enemyPrefabs;

    void Start()
    {
        GenerateCity();
        PlacePlayer();
        PlaceEnemies();
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
        // Calcular la posición de la esquina inferior izquierda y restar un pequeño desplazamiento hacia la izquierda
        float offsetToLeft = 1.0f;
        float offsetToDown = 1.0f; // Desplazamiento a la izquierda, ajustable según sea necesario
        Vector3 playerStartPosition = new Vector3(
            cityCenter.x + (widthBuildings * spaceBetweenElements) / 2 - offsetToLeft, // Resta el offset aquí
            player.transform.position.y, // Mantén la altura original del jugador
            cityCenter.z + (heightBuildings * spaceBetweenElements) / 2 - offsetToDown
        );
        player.transform.position = playerStartPosition;
    }
    else
    {
        Debug.LogError("Player object is not assigned in the inspector!");
    }
}

void PlaceEnemies()
{
    if (enemyPrefabs.Length < 3)
    {
        Debug.LogError("Not enough enemy prefabs assigned!");
        return;
    }

    // Calcula posiciones para los tres enemigos
    Vector3[] enemyPositions = new Vector3[3];
    enemyPositions[0] = new Vector3(cityCenter.x - (widthBuildings * spaceBetweenElements) / 2.85f, cityCenter.y = 0.1f, cityCenter.z - (heightBuildings * spaceBetweenElements) / 2.5f); // Esquina inferior izquierda
    enemyPositions[1] = new Vector3(cityCenter.x + (widthBuildings * spaceBetweenElements) / 2.22f, cityCenter.y = 0.1f, cityCenter.z - (heightBuildings * spaceBetweenElements) / 2.5f); // Esquina superior derecha
    enemyPositions[2] = new Vector3(cityCenter.x - (widthBuildings * spaceBetweenElements) / 2.85f, cityCenter.y = 0.1f, cityCenter.z + (heightBuildings * spaceBetweenElements) / 2.5f); // Esquina superior izquierda

    // Instancia cada enemigo en su posición designada
    for (int i = 0; i < enemyPositions.Length; i++)
    {
        Instantiate(enemyPrefabs[i], enemyPositions[i], Quaternion.identity);
    }
}
}