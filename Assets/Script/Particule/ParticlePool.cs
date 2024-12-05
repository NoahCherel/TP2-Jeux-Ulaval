using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public GameObject particlePrefab;
    public int poolSize = 50;
    private List<GameObject> pool;

    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject particle = Instantiate(particlePrefab);
            particle.SetActive(false);
            pool.Add(particle);
        }
    }

    public GameObject GetParticle()
    {
        foreach (var particle in pool)
        {
            if (!particle.activeInHierarchy)
            {
                particle.SetActive(true);
                return particle;
            }
        }

        GameObject newParticle = Instantiate(particlePrefab);
        newParticle.SetActive(true);
        pool.Add(newParticle);
        return newParticle;
    }

    public void ReturnParticle(GameObject particle)
    {
        particle.SetActive(false);
    }
}
