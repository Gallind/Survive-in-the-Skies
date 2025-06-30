
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector3 spawnPosition = new Vector3(40, 0, 17);
    public float spawnRadius = 10f; // Radius to search for a valid NavMesh position

    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;

    public float minDuration = 10f;
    public float maxDuration = 20f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    System.Collections.IEnumerator SpawnEnemies()
    {
        float duration = Random.Range(minDuration, maxDuration);
        float timer = 0f;

        while (timer < duration)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            // Generate a random point within the spawn radius
            Vector3 randomPoint = spawnPosition + Random.insideUnitSphere * spawnRadius;

            // Find a valid position on the NavMesh near the randomPoint
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, spawnRadius, NavMesh.AllAreas))
            {
                Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"Could not find a valid position on the NavMesh to spawn the enemy near {randomPoint}");
            }

            //timer += waitTime;
        }
    }
}
