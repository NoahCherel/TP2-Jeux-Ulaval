using UnityEngine;
using Unity.Netcode;

public class ProjectileMulti : NetworkBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private float lifetime = 5f; // Time before the projectile is destroyed

    private void Start()
    {
        if (IsOwner)
        {
            // Destroy the projectile after its lifetime
            Destroy(gameObject, lifetime);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            // Start the projectile movement on the owner client
            MoveProjectile();
        }
    }

    private void MoveProjectile()
    {
        // Move the projectile using Transform.Translate
        // You can also use Vector3.forward or transform.forward to move along the object's forward direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void Update()
    {
        // Ensure only the owner controls the movement
        if (IsOwner)
        {
            // Continue moving the projectile
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
