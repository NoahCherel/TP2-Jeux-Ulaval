using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;         // Enemy prefab to spawn
    public GameObject parentObject;        // Parent GameObject for enemies
    public int initialEnemiesPerWave = 5;  // Number of enemies in the first wave
    public int enemyIncreasePerWave = 2;   // How many additional enemies per wave
    public float spawnInterval = 3f;       // Time between waves
    public float spawnAreaWidth = 10f;     // Width of the spawn area
    public float spawnAreaHeight = 10f;    // Height (depth) of the spawn area

    private int currentWave = 0;           // Current wave number

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++; // Increment wave number
            int enemiesToSpawn = initialEnemiesPerWave + (currentWave - 1) * enemyIncreasePerWave;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f); // Delay between individual enemy spawns
            }

            // Wait until the next wave
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Generate a random position within the defined area relative to the WaveManager's position
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f),
            transform.position.y + 1f, // Slightly above the ground
            transform.position.z + Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f)
        );

        // Instantiate the enemy at the chosen spawn position with parent set to parentObject
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Set the parent of the enemy to the specified parentObject
        if (parentObject != null)
        {
            enemy.transform.parent = parentObject.transform;
        }
    }
}
