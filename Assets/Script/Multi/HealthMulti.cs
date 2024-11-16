using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class HealthMulti : NetworkBehaviour
{
    public int maxHealth = 100;
    private NetworkVariable<int> currentHealth = new NetworkVariable<int>(100);
    public bool isDead = false;
    public TMP_Text healthText;

    private void Start()
    {
        // Disable the UI for remote players
        if (!IsOwner)
        {
            healthText.gameObject.SetActive(false);
        }

        if (IsOwner)
        {
            UpdateHealthText(currentHealth.Value);
        }

        // Listen for health updates from the server
        currentHealth.OnValueChanged += (prevValue, newValue) =>
        {
            UpdateHealthText(newValue);
            CheckIfDead(newValue);
        };
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner && healthText != null)
        {
            UpdateHealthText(currentHealth.Value);
        }
    }

    // Method to take damage (can be called by other players or server)
    [ServerRpc(RequireOwnership = false)]
    public void TakeDamageServerRpc(int damage, ServerRpcParams serverRpcParams = default)
    {
        if (isDead) return;

        currentHealth.Value -= damage;
        currentHealth.Value = Mathf.Clamp(currentHealth.Value, 0, maxHealth);

        CheckIfDead(currentHealth.Value);
    }

    // Heal the player (server-side control)
    [ServerRpc(RequireOwnership = false)]
    public void HealServerRpc(int healAmount, ServerRpcParams serverRpcParams = default)
    {
        if (isDead) return;

        currentHealth.Value += healAmount;
        currentHealth.Value = Mathf.Clamp(currentHealth.Value, 0, maxHealth);
    }

    private void CheckIfDead(int health)
    {
        if (health <= 0 && !isDead)
        {
            isDead = true;
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Debug.Log($"Player {OwnerClientId} has died.");
    }

    private void UpdateHealthText(int health)
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {health}";
        }
    }
}
