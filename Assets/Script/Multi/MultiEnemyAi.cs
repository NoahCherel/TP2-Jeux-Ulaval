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

    private List<FPSController> players = new List<FPSController>(); // To store player controllers

    void Start()
    {
        if (!IsServer) return; // Only run on server

        // Find all players and add them to the list
        FindAllPlayers();
    }

    void Update()
    {
        if (!IsServer) return; // Only run on server

        Transform targetPlayer = GetClosestPlayer();

        if (targetPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

            // Move towards the player if within detection range
            if (distanceToPlayer <= detectionRange)
            {
                MoveTowardsPlayer(targetPlayer);

                // Attack the player if within attack range and cooldown is finished
                if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
                {
                    Attack(targetPlayer);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    // Finds all players in the game
    void FindAllPlayers()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (var playerObject in playerObjects)
        {
            FPSController fpsController = playerObject.GetComponent<FPSController>();
            if (fpsController != null && !players.Contains(fpsController))
            {
                players.Add(fpsController);
            }
        }
    }

    // Finds the closest player
    Transform GetClosestPlayer()
    {
        Transform closestPlayer = null;
        float closestDistance = float.MaxValue;

        foreach (var player in players)
        {
            if (player != null) // Ensure player is valid
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToPlayer < closestDistance)
                {
                    closestDistance = distanceToPlayer;
                    closestPlayer = player.transform;
                }
            }
        }

        return closestPlayer;
    }

    // Moves the enemy towards the target player
    void MoveTowardsPlayer(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;

        // Avoid obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                direction = Vector3.Cross(direction, Vector3.up).normalized; // Adjust direction to avoid obstacle
            }
        }

        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player);
    }

    // Attack method
    void Attack(Transform player)
    {
        HealthMulti playerHealth = player.GetComponent<HealthMulti>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
