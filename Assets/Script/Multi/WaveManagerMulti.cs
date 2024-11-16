using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class WaveManagerMulti : NetworkBehaviour
{
    public GameObject mobPrefab;
    public int mobsPerWave = 5;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 10f;
    private int waveIndex = 0;

    void Update()
    {
        if (!IsServer) // Only the server should handle spawning
        {
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < mobsPerWave; i++)
        {
            SpawnMob();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnMob()
    {
        Vector3 spawnPosition = new Vector3(
            transform.position.x + Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f),
            transform.position.y + 1f, // Slightly above the ground
            transform.position.z + Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f)
        );

        // Instantiate the mob and spawn it on the network
        GameObject mob = Instantiate(mobPrefab, spawnPosition, Quaternion.identity);
        NetworkObject networkObject = mob.GetComponent<NetworkObject>();

        // Spawn the object on the network for all clients
        networkObject.Spawn();
    }
}
