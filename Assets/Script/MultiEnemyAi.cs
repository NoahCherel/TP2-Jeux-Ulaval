using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class MultiEnemyAI : NetworkBehaviour
{
    public float detectionRange = 10f;
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public int damage = 10;
    public float attackCooldown = 1.5f;

    public delegate void EnemyDestroyed();
    public event EnemyDestroyed OnDestroyed;

    private float nextAttackTime = 0f;

    private List<PlayerHealth> playerHealths = new List<PlayerHealth>();  // To store player health components

    void Start()
    {
        if (!IsServer) return;  // Only run on server

        // Initialize the player health list
        FindAllPlayers();

        // Wait until at least 2 players are in the game
        if (playerHealths.Count < 2)
        {
            Debug.Log("Waiting for more players to join...");
        }
    }

    void Update()
    {
        if (!IsServer) return; // Only run on server

        // Ensure we wait until at least 2 players are connected
        if (playerHealths.Count < 2)
        {
            return; // Do nothing if there are not enough players
        }

        Transform targetPlayer = GetClosestPlayer();

        if (targetPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

            // If the player is within detection range
            if (distanceToPlayer <= detectionRange)
            {
                MoveTowardsPlayer(targetPlayer);

                // If the player is within attack range and cooldown is finished
                if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
                {
                    Attack(targetPlayer);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    // Finds all players currently in the game (with "Player" tag)
    void FindAllPlayers()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (var playerObject in playerObjects)
        {
            PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.isDead && !playerHealths.Contains(playerHealth))
            {
                playerHealths.Add(playerHealth);
            }
        }
    }

    // Finds the closest player who is not dead
    Transform GetClosestPlayer()
    {
        Transform closestPlayer = null;
        float closestDistance = float.MaxValue;

        foreach (var playerHealth in playerHealths)
        {
            if (playerHealth != null && !playerHealth.isDead)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerHealth.transform.position);
                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestPlayer = playerHealth.transform;
                }
            }
        }

        return closestPlayer;
    }

    // Moves the enemy towards the target player
    void MoveTowardsPlayer(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Avoid obstacles (simple logic here)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                direction = Vector3.Cross(direction, Vector3.up).normalized; // Adjust direction to avoid obstacle
            }
        }

        // Move towards the player
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player);
    }

    // Attack method (damage dealt to the player)
    void Attack(Transform player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
