using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float detectionRange = 10f;
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public int damage = 10;
    public float attackCooldown = 1.5f;

    private float nextAttackTime = 0f;

    void Update()
    {
        // Sélectionner le joueur le plus proche
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
        if (player1 == null && player2 == null)
            return null;

        float distanceToPlayer1 = player1 ? Vector3.Distance(transform.position, player1.position) : float.MaxValue;
        float distanceToPlayer2 = player2 ? Vector3.Distance(transform.position, player2.position) : float.MaxValue;

        return (distanceToPlayer1 < distanceToPlayer2) ? player1 : player2;
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
