
using UnityEngine;

using System.Collections.Generic;

public class FoodSpawner : MonoBehaviour
{
    public static List<GameObject> spawnedFoodObjects = new List<GameObject>();
    public int maxFoodObjects = 10;

    public GameObject foodPrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating("SpawnFood", spawnInterval, spawnInterval);
    }

    void SpawnFood()
    {
        if (foodPrefab != null && spawnedFoodObjects.Count < maxFoodObjects)
        {
            Vector3 spawnCenter = new Vector3(40, 0, 15);
            Vector3 randomPosition = spawnCenter + Random.insideUnitSphere * spawnRadius;
            randomPosition.y = Terrain.activeTerrain.SampleHeight(randomPosition);
            GameObject newFood = Instantiate(foodPrefab, randomPosition, Quaternion.identity);
            spawnedFoodObjects.Add(newFood);
        }
    }
}
