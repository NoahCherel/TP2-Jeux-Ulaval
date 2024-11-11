using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Référence au personnage à suivre
    public Vector3 offset = new Vector3(0, 10, 0); // Distance de la caméra par rapport au joueur

    void LateUpdate()
    {
        if (player != null)
        {
            // Place la caméra à la position du joueur avec un décalage
            transform.position = player.position + offset;
        }
    }
}
