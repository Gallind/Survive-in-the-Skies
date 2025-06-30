
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating("SpawnFood", spawnInterval, spawnInterval);
    }

    void SpawnFood()
    {
        if (foodPrefab != null)
        {
            Vector3 spawnCenter = new Vector3(40, 0, 15);
            Vector3 randomPosition = spawnCenter + Random.insideUnitSphere * spawnRadius;
            randomPosition.y = Terrain.activeTerrain.SampleHeight(randomPosition);
            Instantiate(foodPrefab, randomPosition, Quaternion.identity);
        }
    }
}
