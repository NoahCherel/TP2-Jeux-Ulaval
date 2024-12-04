using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;    
    public Sound[] musicSounds, sfxSounds, FoleySounds;
    public AudioSource musicSource, sfxSource, foleySource, musicSpacialSource, sfxSpacialSource, foleySpacialSource;
    public static event Action<float> OnFoleyVolumeChanged;

    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string FOLEY_VOLUME_KEY = "FoleyVolume";

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

    private void Start()
    {
        PlaySpacialMusic("MainMenu");
        // Charger les volumes sauvegardés au démarrage
        LoadVolumeSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayRandomFoley();
        }
    }

    // Sauvegarder les réglages de volume
    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicSource.volume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxSource.volume);
        PlayerPrefs.SetFloat(FOLEY_VOLUME_KEY, foleySource.volume);
        PlayerPrefs.Save(); // Sauvegarder immédiatement
    }

    public void PlayMusic(string soundName)
    {
        Sound sound = Array.Find(musicSounds, s => s.soundName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        musicSource.clip = sound.clip;
        musicSource.Play();
    }

    public void PlaySFX(string soundName)
    {
        Sound sound = Array.Find(sfxSounds, s => s.soundName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        sfxSource.PlayOneShot(sound.clip);
    }

    public void PlayFoley(string soundName)
    {
        Sound sound = Array.Find(FoleySounds, s => s.soundName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        foleySource.PlayOneShot(sound.clip);
    }

    public void PlaySpacialMusic(string soundName)
    {
        Sound sound = Array.Find(musicSounds, s => s.soundName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        musicSpacialSource.clip = sound.clip;
        musicSpacialSource.Play();
    }

    public void PlaySpacialSFX(string soundName)
    {
        Sound sound = Array.Find(sfxSounds, s => s.soundName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        sfxSpacialSource.PlayOneShot(sound.clip);
    }

    public void PlaySpacialFoley(string soundName)
    {
        Sound sound = Array.Find(FoleySounds, s => s.soundName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        foleySpacialSource.PlayOneShot(sound.clip);
    }

    public void StopMusic(string soundName)
    {
        Sound sound = Array.Find(musicSounds, s => s.soundName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        musicSource.Stop();
    }

    public void StopSpacialMusic(string soundName)
    {
        Sound sound = Array.Find(musicSounds, s => s.soundName == soundName);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        musicSpacialSource.Stop();
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSpacialSource.volume = volume;

        SaveVolumeSettings(); // Sauvegarder après modification
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        sfxSpacialSource.volume = volume;

        SaveVolumeSettings(); // Sauvegarder après modification
    }

    public void FoleyVolume(float volume)
    {
        foleySource.volume = volume;
        foleySpacialSource.volume = volume;
        OnFoleyVolumeChanged?.Invoke(volume);

        SaveVolumeSettings(); // Sauvegarder après modification
    }

    private void LoadVolumeSettings()
    {
        // Charger les volumes depuis PlayerPrefs, avec des valeurs par défaut (1 signifie volume maximal)
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);
        float foleyVolume = PlayerPrefs.GetFloat(FOLEY_VOLUME_KEY, 1f);

        // Appliquer les volumes
        musicSource.volume = musicVolume;
        musicSpacialSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
        sfxSpacialSource.volume = sfxVolume;
        foleySource.volume = foleyVolume;
        foleySpacialSource.volume = foleyVolume;

        Debug.Log("Music Volume: " + musicVolume + ", SFX Volume: " + sfxVolume + ", Foley Volume: " + foleyVolume);

        // Notifier tout autre système qui pourrait écouter le volume Foley
        OnFoleyVolumeChanged?.Invoke(foleyVolume);

        // Actualiser les sliders pour afficher les valeurs chargées
        if (UIController.instance != null)
        {
            UIController.instance.UpdateSliders(musicVolume, sfxVolume, foleyVolume);
        }
    }

    public void PlayRandomFoley()
    {
        FoleySoundManager.instance.PlayRandomFoley();
    }
}
