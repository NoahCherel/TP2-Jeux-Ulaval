using UnityEngine;
using System;

public class FoleySoundManager : MonoBehaviour
{
    public static FoleySoundManager instance;
    public Sound[] FoleySounds; // Liste des sons de Foley
    public AudioSource foleySource; // Source audio pour jouer les sons Foley

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Méthode pour jouer un son de Foley aléatoire
    public void PlayRandomFoley()
    {
        if (FoleySounds.Length == 0)
        {
            Debug.LogWarning("No Foley sounds available to play!");
            return;
        }

        // Choisir un indice aléatoire parmi les sons Foley
        int randomIndex = UnityEngine.Random.Range(0, FoleySounds.Length);

        // Récupérer le son aléatoire basé sur l'indice
        Sound randomSound = FoleySounds[randomIndex];

        if (randomSound != null)
        {
            // Jouer le son
            foleySource.PlayOneShot(randomSound.clip);
        }
        else
        {
            Debug.LogWarning("Foley sound is null at index: " + randomIndex);
        }
    }
}
