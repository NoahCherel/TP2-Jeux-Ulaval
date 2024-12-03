using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicVolumeSlider;   // Slider pour le volume de la musique
    public Slider sfxVolumeSlider;     // Slider pour le volume des effets sonores

    // Fonction appelée pour sauvegarder les réglages de volume
    public void SaveVolumeSettings()
    {
        // Sauvegarde les volumes actuels dans PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);

        // Applique les réglages de volume à SoundManager
        SoundManager.instance.SetMusicVolume(musicVolumeSlider.value);
        SoundManager.instance.SetSFXVolume(sfxVolumeSlider.value);
    }

    // Fonction pour charger les réglages de volume
    public void LoadVolumeSettings()
    {
        // Si les réglages ont déjà été sauvegardés
        if (PlayerPrefs.HasKey("MusicVolume") && PlayerPrefs.HasKey("SFXVolume"))
        {
            // Charge les volumes sauvegardés
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");

            // Applique les volumes sauvegardés au SoundManager
            SoundManager.instance.SetMusicVolume(musicVolumeSlider.value);
            SoundManager.instance.SetSFXVolume(sfxVolumeSlider.value);
        }
        else
        {
            // Si aucun réglage n'est trouvé, on applique les valeurs par défaut
            musicVolumeSlider.value = 1f;
            sfxVolumeSlider.value = 1f;

            // Applique les volumes par défaut au SoundManager
            SoundManager.instance.SetMusicVolume(1f);
            SoundManager.instance.SetSFXVolume(1f);
        }
    }

    void Start()
    {
        // Charge les réglages de volume au démarrage
        LoadVolumeSettings();
    }
}
