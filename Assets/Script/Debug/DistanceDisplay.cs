using UnityEngine;
using TMPro; // Nécessaire pour TextMeshPro (facultatif si tu affiches juste dans la console)

public class DistanceDisplay : MonoBehaviour
{
    public Transform player;       // Référence au joueur
    public string enemyTag = "Enemy"; // Tag utilisé pour identifier les ennemis
    public float interval = 2f;    // Intervalle en secondes entre chaque affichage

    private float timer;           // Chronomètre interne

    void Update()
    {
        // Met à jour le chronomètre
        timer += Time.deltaTime;

        // Si le chronomètre dépasse l'intervalle, calcule les distances
        if (timer >= interval)
        {
            // Trouve tous les objets avec le tag "Enemy"
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            foreach (GameObject enemy in enemies)
            {
                // Calcule la distance entre le joueur et cet ennemi
                float distance = Vector3.Distance(player.position, enemy.transform.position);

                // Affiche la distance dans la console
                Debug.Log($"Distance entre le joueur et {enemy.name} : {distance:F2} unités");
            }

            // Réinitialise le chronomètre
            timer = 0f;
        }
    }
}
