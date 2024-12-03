using UnityEngine;

public class MovingSound : MonoBehaviour
{
    public Transform player; // Référence au joueur ou caméra
    public float radius = 5f; // Rayon du mouvement circulaire
    public float speed = 1f; // Vitesse de rotation
    public float verticalAmplitude = 2f; // Amplitude du mouvement vertical
    public float verticalSpeed = 1f; // Vitesse du mouvement vertical

    public bool clockwise = true; // Choix du sens de rotation (horaire ou antihoraire)

    void Update()
    {
        if (clockwise)
        {
            MoveClockwise();
        }
        else
        {
            MoveCounterClockwise();
        }
    }

    // Mouvement circulaire horaire (sens horaire)
    void MoveClockwise()
    {
        float x = player.position.x + Mathf.Cos(Time.time * speed) * radius;
        float z = player.position.z + Mathf.Sin(Time.time * speed) * radius;
        float y = player.position.y + Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude;
        transform.position = new Vector3(x, y, z);
    }

    // Mouvement circulaire antihoraire (sens inverse)
    void MoveCounterClockwise()
    {
        float x = player.position.x - Mathf.Cos(Time.time * speed) * radius; // Inverser le signe ici
        float z = player.position.z - Mathf.Sin(Time.time * speed) * radius; // Inverser le signe ici
        float y = player.position.y + Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude;
        transform.position = new Vector3(x, y, z);
    }
}
