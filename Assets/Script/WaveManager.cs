using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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

    public int currentWave = 0;           // Current wave number
    private List<GameObject> activeEnemies = new List<GameObject>();  // List to track active enemies

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            // Wait until all enemies from the previous wave are destroyed
            yield return new WaitUntil(() => activeEnemies.Count == 0);

            // Prepare for the next wave
            currentWave++;
            int enemiesToSpawn = initialEnemiesPerWave + (currentWave - 1) * enemyIncreasePerWave;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f); // Delay between individual spawns
            }
        }
    }

    void SpawnEnemy()
    {
        // Generate a random spawn position within the defined area, relative to the WaveManager's position
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f),
            transform.position.y + 1f, // Slightly above the ground
            transform.position.z + Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f)
        );

        // Instantiate the enemy at the chosen spawn position and rotation
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Add the enemy to the list of active enemies
        activeEnemies.Add(enemy);

        if (enemyPrefab.name != "MonsterMulti")
        {
            enemy.transform.SetParent(parentObject.transform);
        }

        // Add a destruction event to remove the enemy from the active list when it is destroyed
        if (enemyPrefab.name == "MonsterMulti")
        {
            enemy.GetComponent<MultiEnemyAI>().OnDestroyed += () => activeEnemies.Remove(enemy);
        }
        else
        {
            enemy.GetComponent<EnemyAI>().OnDestroyed += () => activeEnemies.Remove(enemy);
        }
    }
}
