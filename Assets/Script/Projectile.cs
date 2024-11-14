using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Vitesse du projectile
    public float lifeTime = 2f; // Durée de vie avant destruction

    private PlayerScore playerScore;
    public int playerID;

    void Start()
    {
        // Récupérer la référence du PlayerScore spécifique au joueur qui a tiré le projectile
        if (playerID == 1)
        {
            playerScore = GameObject.Find("Capsule").GetComponent<PlayerScore>();
        }
        else
        {
            playerScore = GameObject.Find("Capsule2").GetComponent<PlayerScore>();
        }

        // Détruire le projectile après un certain temps pour éviter de surcharger la scène
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Déplacer le projectile vers l'avant
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Détection des collisions avec des ennemis ou des obstacles
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Gestion de l'impact avec un ennemi
            Destroy(other.gameObject); // Détruire l'ennemi
            Score();
            Destroy(gameObject); // Détruire le projectile
        }
    }

    void Score()
    {
        if (playerScore != null)
        {
            playerScore.AddScore(10, playerID);
        }
    }
}
