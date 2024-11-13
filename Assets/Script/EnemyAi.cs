using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player1;    
    public GameObject player2;    
    public float detectionRange = 10f;
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public int damage = 10;
    public float attackCooldown = 1.5f;

    private float nextAttackTime = 0f;

    void Update()
    {
        player1 = GameObject.Find("Capsule");
        player2 = GameObject.Find("Capsule2");

        Transform targetPlayer = GetClosestPlayer();

        if (targetPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, targetPlayer.position);

            // Si le joueur est dans la portée de détection
            if (distanceToPlayer <= detectionRange)
            {
                MoveTowardsPlayer(targetPlayer);

                // Si le joueur est dans la portée d'attaque
                if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
                {
                    Attack(targetPlayer);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    Transform GetClosestPlayer()
    {
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.transform.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.transform.position);

        if (distanceToPlayer1 < distanceToPlayer2)
        {
            return player1.transform;
        }
        else
        {
            return player2.transform;
        }
    }

    void MoveTowardsPlayer(Transform player)
    {
        // Direction vers le joueur
        Vector3 direction = (player.position - transform.position).normalized;

        // Mouvement avec détection basique d'obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                // Évite les obstacles en ajustant la direction
                direction = Vector3.Cross(direction, Vector3.up).normalized;
            }
        }

        // Applique le mouvement vers le joueur
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player);
    }

    void Attack(Transform player)
    {
        // Appliquer des dégâts au joueur
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
