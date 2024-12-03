using UnityEngine;

public class MonsterAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        // Récupérer ou ajouter un AudioSource sur ce GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Configurer l'AudioSource pour un son en boucle
        audioSource.loop = true; 
        audioSource.spatialBlend = 1.0f; // 1 = son entièrement spatial
        audioSource.playOnAwake = false; // Pas de lecture automatique au démarrage
    }

    private void Start()
    {
        // Jouer le son en continu
        audioSource.Play();
    }

    private void OnEnable()
    {
        // S'abonner à l'événement pour synchroniser le volume Foley
        SoundManager.OnFoleyVolumeChanged += UpdateFoleyVolume;
    }

    private void OnDisable()
    {
        // Se désabonner de l'événement
        SoundManager.OnFoleyVolumeChanged -= UpdateFoleyVolume;
    }

    // Synchroniser le volume avec celui des sons Foley
    private void UpdateFoleyVolume(float volume)
    {
        audioSource.volume = volume;
    }

    // Méthode pour configurer le clip audio
    public void SetFoleySound(AudioClip clip)
    {
        audioSource.clip = clip;
    }
}
