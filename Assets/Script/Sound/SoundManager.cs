using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton pour y accéder facilement
    private AudioSource musicSource; // Source pour la musique
    private AudioSource sfxSource;   // Source pour les effets sonores

    public AudioClip FirstClickSound;
    public AudioClip nextSound;
    public AudioClip backSound;
    public AudioClip EnemyWalkSound;

    // Musique à jouer au démarrage
    public AudioClip startMusicClip; // Musique de démarrage, à assigner dans l'inspecteur
    public AudioClip scene1Music;    // Musique pour la scène 1

    [Range(0f, 1f)] public float musicVolume = 1f;  // Volume de la musique
    [Range(0f, 1f)] public float sfxVolume = 1f;     // Volume des effets sonores

    public Slider musicSlider;   // Slider pour le volume de la musique
    public Slider sfxSlider;     // Slider pour le volume des effets sonores

    private void Awake()
    {
        // Vérifie s'il existe déjà une instance, sinon l'initialise
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Garde le SoundManager entre les scènes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Assigner les sources audio
        musicSource = GetComponent<AudioSource>(); 
        sfxSource = gameObject.AddComponent<AudioSource>(); 

        // Applique les volumes par défaut
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;

        // Initialise les sliders avec les valeurs actuelles
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        // Ajoute les listeners pour changer les volumes en temps réel
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Jouer la musique de démarrage si elle est assignée
        if (startMusicClip != null)
        {
            PlayMusic(startMusicClip);
        }
    }

    // Fonction pour jouer un son (effet sonore)
    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip); // Joue un son sans interrompre ceux en cours
    }

    // Fonction pour jouer la musique
    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource.isPlaying) 
        {
            musicSource.Stop(); // Si une musique est déjà en train de jouer, on la stoppe avant de commencer une nouvelle
        }

        musicSource.clip = musicClip;   // Définir le clip de musique à jouer
        musicSource.Play();             // Lancer la musique
        musicSource.volume = musicVolume; // Appliquer le volume de la musique
    }

    // Fonction pour ajuster le volume de la musique
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume; // Modifie le volume de la musique
    }

    // Fonction pour ajuster le volume des effets sonores
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = sfxVolume; // Modifie le volume des effets sonores
    }
}
