using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ScoreMulti : NetworkBehaviour
{
    private NetworkVariable<int> currentScore = new NetworkVariable<int>(0);
    public TMP_Text scoreText;

    private void Start()
    {
        if (!IsOwner)
        {
            scoreText.gameObject.SetActive(false);
        }
        // Update the score display when the score changes
        currentScore.OnValueChanged += (prevValue, newValue) =>
        {
            UpdateScoreText(newValue);
        };
    }

    public override void OnNetworkSpawn()
    {
        // Ensure score text is updated for new clients
        if (scoreText != null)
        {
            UpdateScoreText(currentScore.Value);
        }
    }

    // Method to increase the score
    [ServerRpc(RequireOwnership = false)]
    public void AddScoreServerRpc(int points, ServerRpcParams serverRpcParams = default)
    {
        if (points <= 0)
        {
            Debug.LogWarning("Invalid score value received.");
            return;
        }

        currentScore.Value += points;
    }

    // Method to reset the score
    [ServerRpc(RequireOwnership = false)]
    public void ResetScoreServerRpc(ServerRpcParams serverRpcParams = default)
    {
        currentScore.Value = 0;
    }

    private void UpdateScoreText(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}
