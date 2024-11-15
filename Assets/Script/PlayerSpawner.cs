using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    private void Start()
    {
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager is not initialized.");
            return;
        }

        // Spawn all players at the start if the server is active
        if (NetworkManager.Singleton.IsServer)
        {
            SpawnAllPlayers();
        }

        // Register the client connected callback
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
    }

    private void OnDestroy()
    {
        // Unregister the callback to prevent memory leaks
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
        }
    }

    private void SpawnAllPlayers()
    {
        foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            SpawnPlayer(clientId);
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        Debug.Log($"Client connected: {clientId}");
        if (NetworkManager.Singleton.IsServer)
        {
            SpawnPlayer(clientId);
        }
    }

    private void SpawnPlayer(ulong clientId)
    {
        var spawnPosition = GetRandomSpawnPosition();
        var playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        var networkObject = playerInstance.GetComponent<NetworkObject>();

        if (networkObject != null)
        {
            networkObject.SpawnAsPlayerObject(clientId);
        }
        else
        {
            Debug.LogError("The player prefab does not have a NetworkObject component.");
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-5, 5), 2, Random.Range(-5, 5));
    }
}
