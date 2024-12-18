using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    public ParticlePool particlePool; // Référence au pool de particules

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Lors de l'appui sur Espace
        {
            Debug.Log("Space key was pressed.");
            GameObject particle = particlePool.GetParticle(); // Récupère une particule du pool

            if (particle != null)
            {
                var particleSystem = particle.GetComponent<ParticleSystem>(); // Récupération du ParticleSystem
                if (particleSystem != null)
                {
                    // Inverser l'état du système de particules (activer/désactiver)
                    particle.SetActive(true); 

                    // if (particleSystem.isPlaying)
                    // {
                    //     particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    //     Debug.Log("Effet de particule désactivé.");
                    // }
                    // else
                    // {
                    //     particleSystem.Play();
                    //     Debug.Log("Effet de particule activé.");
                    // }
                }
                else
                {
                    Debug.LogError("Aucun ParticleSystem attaché au GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("Aucune particule disponible dans le pool.");
            }
        }
    }
}
