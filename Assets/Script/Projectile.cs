using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Vitesse du projectile
    public float lifeTime = 2f; // Durée de vie avant destruction

    void Start()
    {
        // Détruire le projectile après un certain temps pour éviter de surcharger la scène
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Déplacer le projectile vers l'avant
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Détection des collisions avec des ennemis ou des obstacles (optionnel)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Gestion de l'impact avec un ennemi
            Destroy(other.gameObject); // Exemple de destruction de l'ennemi
            Destroy(gameObject); // Détruire le projectile
        }
    }
}
