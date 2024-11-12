using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public float moveSpeed = 6f; // Vitesse de déplacement du joueur
    private bool isBlocked = false; // Détermine si le joueur est bloqué par une collision

    void Update()
    {
        // Si le joueur n'est pas bloqué, il peut se déplacer
        if (!isBlocked)
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // Détection des entrées de mouvement
        horizontal = Input.GetKey(KeyCode.A) ? -1 : (Input.GetKey(KeyCode.D) ? 1 : 0);
        vertical = Input.GetKey(KeyCode.W) ? 1 : (Input.GetKey(KeyCode.S) ? -1 : 0);

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    // Méthode appelée lorsqu'une collision se produit
    private void OnCollisionEnter(Collision collision)
    {
        // Vérifie si la collision est avec un cube (ou un autre objet spécifique)
        if (collision.gameObject.CompareTag("Cube"))
        {
            isBlocked = true;  // Bloque le mouvement du joueur
            Debug.Log("Le joueur est bloqué par un cube !");
        }
    }

    // Méthode appelée lorsqu'une collision se termine (si tu veux lever le blocage)
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            isBlocked = false;  // Débloque le mouvement du joueur
            Debug.Log("Le joueur peut se déplacer à nouveau !");
        }
    }
}
