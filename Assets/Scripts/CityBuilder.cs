using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    public Vector3 cityCenter;
    public int widthBuildings = 10;
    public int heightBuildings = 10;
    public float spaceBetweenElements = 2.0f;
    public GameObject[] buildingPrefabs;
    public GameObject roadPrefab;
    public GameObject[] stuffPrefabs;
    public float possibilityNotBuilding = 0.4f;

    void Start()
    {
        GenerateCity();
    }

    void GenerateCity()
    {
        Vector3 position = cityCenter;

        for (int x = 0; x < widthBuildings; x++)
        {
            if (x % 2 == 0) //verifica si x es par
            {
                for (int y = 0; y < heightBuildings; y++) //bucle for en z
                {
                    if (Random.value > possibilityNotBuilding) //compara y añade un edificio
                    {
                        GameObject prefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
                        Instantiate(prefab, position, Quaternion.identity);
                    }
                    else if (stuffPrefabs.Length > 0)
                    {
                        GameObject stuffPrefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
                        Instantiate(stuffPrefab, position, Quaternion.identity);
                    }
                    position.z += spaceBetweenElements; //la posicionen z se incrementa para colocar el siguiente objeto
                }
            }
            else //si es impar se ejecuta "else", generando una carretera
            {
                for (int y = 0; y < heightBuildings; y++)
                {
                    Instantiate(roadPrefab, position, Quaternion.identity);
                    position.z += spaceBetweenElements;
                }
            }
            position.z = cityCenter.z;
            position.x += spaceBetweenElements;
        }
    }
}