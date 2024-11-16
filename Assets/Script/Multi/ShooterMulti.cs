using UnityEngine;
using Unity.Netcode;

public class ShooterMulti : NetworkBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOwner)
        {
            ShootServerRpc(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }

    [ServerRpc]
    private void ShootServerRpc(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        bullet.GetComponent<NetworkObject>().Spawn();
    }
}
