using UnityEngine;

public class FoleySoundManager : MonoBehaviour
{
    public static FoleySoundManager instance;
    public Sound[] Emote;
    public Sound[] StartGame;
    public Sound[] CompleteWave;
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
    public void PlayRandomFoley(Sound[] customFoleySounds)
    {
        // Vérifier si la liste fournie est valide
        if (customFoleySounds == null || customFoleySounds.Length == 0)
        {
            Debug.LogWarning("No Foley sounds available to play!");
            return;
        }

        // Choisir un indice aléatoire parmi les sons disponibles
        int randomIndex = UnityEngine.Random.Range(0, customFoleySounds.Length);

        // Récupérer le son aléatoire basé sur l'indice
        Sound randomSound = customFoleySounds[randomIndex];

        if (randomSound != null && randomSound.clip != null)
        {
            // Jouer le son
            foleySource.PlayOneShot(randomSound.clip);
        }
        else
        {
            Debug.LogWarning("Foley sound or clip is null at index: " + randomIndex);
        }
    }

    // Méthodes spécifiques pour jouer des sons à partir des listes Emote et StartGame
    public void PlayRandomEmote()
    {
        PlayRandomFoley(Emote);
    }

    public void PlayRandomStartGame()
    {
        PlayRandomFoley(StartGame);
    }
}
