using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManagerMulti : NetworkBehaviour
{
    // Liste pour suivre si chaque joueur est mort
    private Dictionary<ulong, bool> playerStates = new Dictionary<ulong, bool>();

    // Référence à EndGameManager
    public EndGameManagerMulti endGameManagerMulti;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnPlayerConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnPlayerDisconnected;
        }
    }

    private void OnPlayerConnected(ulong clientId)
    {
        // Lorsqu'un joueur se connecte, on ajoute son état (par défaut vivant)
        if (!playerStates.ContainsKey(clientId))
        {
            playerStates.Add(clientId, false); // false = pas mort
            Debug.Log($"Joueur {clientId} ajouté à playerStates.");
        }
    }

    private void OnPlayerDisconnected(ulong clientId)
    {
        // Supprimer le joueur de la liste lorsqu'il se déconnecte
        if (playerStates.ContainsKey(clientId))
        {
            playerStates.Remove(clientId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdatePlayerStateServerRpc(ulong playerId, bool isDead)
    {
        if (playerStates.ContainsKey(playerId))
        {
            playerStates[playerId] = isDead;
            Debug.Log($"Joueur {playerId} est mort : {isDead}");
            CheckAllPlayersDead();
        }
        else
        {
            Debug.LogWarning($"Le joueur {playerId} n'existe pas dans playerStates.");
        }
    }

    // Vérifie si tous les joueurs sont morts
    private void CheckAllPlayersDead()
    {
        foreach (var state in playerStates.Values)
        {
            if (!state) return; // Si un joueur n'est pas mort, on arrête la vérification
        }

        Debug.Log("Tous les joueurs sont morts !");
        HandleAllPlayersDead();
    }

    private void HandleAllPlayersDead()
    {
        // Logique à exécuter lorsque tous les joueurs sont morts
        Debug.Log("Fin de partie !");

        // Appel au ClientRpc pour afficher le menu de fin de partie sur tous les clients
        ShowEndMenuClientRpc();
    }

    // ClientRpc pour afficher le menu de fin de partie sur tous les clients
    [ClientRpc]
    public void ShowEndMenuClientRpc()
    {
        // Cette méthode sera appelée sur tous les clients
        if (endGameManagerMulti != null)
        {
            endGameManagerMulti.ShowEndMenu();
        }
    }
}
