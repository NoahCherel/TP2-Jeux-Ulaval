using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class ProjectileMulti : NetworkBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private float lifetime = 5f; // Time before the projectile is destroyed
    private NetworkVariable<Vector3> projectilePosition = new NetworkVariable<Vector3>();
    private void Start()
    {
        if (IsOwner)
        {
            // Start the projectile movement on the owner client and spawn it for everyone
            SpawnProjectileServerRpc();
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            // Destroy the projectile after its lifetime
            Destroy(gameObject, lifetime);
        }

        GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnProjectileServerRpc()
    {
        // Move the projectile and ensure it's synchronized across all clients
        MoveProjectileClientRpc();
    }

    [ClientRpc]
    private void MoveProjectileClientRpc()
    {
        // Only move the projectile if it's on the owner side
        if (IsOwner)
        {
            // Start moving the projectile as a coroutine
            StartCoroutine(MoveProjectile());
        }
    }

    // Change MoveProjectile to return IEnumerator for coroutine
    private IEnumerator MoveProjectile()
    {
        while (true)
        {
            // Move the projectile forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Update projectile position across network
            if (IsOwner)
            {
                projectilePosition.Value = transform.position;
            }

            // Wait until the next frame
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the projectile collided with an enemy
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy
            Destroy(other.gameObject);
            // Destroy the projectile
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Ensure only the owner controls the movement
        if (IsOwner)
        {
            // Continue moving the projectile
            MoveProjectile();
        }
        else
        {
            // Sync projectile position to other clients
            transform.position = projectilePosition.Value;
        }
    }
}
