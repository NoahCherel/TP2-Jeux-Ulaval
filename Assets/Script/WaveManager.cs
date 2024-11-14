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
            // Attendre que tous les ennemis de la vague précédente soient éliminés
            yield return new WaitUntil(() => activeEnemies.Count == 0);

            // Préparer la nouvelle vague
            currentWave++;
            int enemiesToSpawn = initialEnemiesPerWave + (currentWave - 1) * enemyIncreasePerWave;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f); // Delay entre les spawns individuels
            }
        }
    }

    void SpawnEnemy()
    {
        // Générer une position aléatoire dans la zone définie, relative à la position de WaveManager
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f),
            transform.position.y + 1f, // Légèrement au-dessus du sol
            transform.position.z + Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f)
        );

        // Instancier l'ennemi à la position de spawn choisie et définir son parent
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Ajouter l'ennemi à la liste des ennemis actifs
        activeEnemies.Add(enemy);

        // Définir le parent de l'ennemi si spécifié
        if (parentObject != null)
        {
            enemy.transform.parent = parentObject.transform;
        }

        // Ajouter un événement de destruction pour supprimer l'ennemi de la liste quand il meurt
        enemy.GetComponent<EnemyAI>().OnDestroyed += () => activeEnemies.Remove(enemy);
    }
}
