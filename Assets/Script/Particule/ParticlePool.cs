using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public GameObject particlePrefab; // Le prefab de la particule
    public int poolSize = 1; // Taille du pool
    public GameObject parentObject;

    private List<GameObject> particlePool; // Liste pour stocker les particules

    void Start()
    {
        // Initialisation du pool
        particlePool = new List<GameObject>();

        if (parentObject == null)
        {
            Debug.LogError("Aucun GameObject parent assigné pour le pool.");
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject particle = Instantiate(particlePrefab);
            particle.transform.SetParent(parentObject.transform);
            particle.SetActive(false);
            particlePool.Add(particle);
        }
    }

    // Méthode pour récupérer une particule du pool
    public GameObject GetParticle()
    {
        Debug.Log("Size of pool: " + particlePool.Count);
        foreach (GameObject particle in particlePool)
        {
            if (!particle.activeInHierarchy) // Trouver une particule inactive
            {
                return particle;
            }
        }

        return null;
    }
}
