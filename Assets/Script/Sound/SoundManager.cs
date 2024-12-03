using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;	
    public Sound[] musicSounds, sfxSounds, FoleySounds;
    public AudioSource musicSource, sfxSource, foleySource;
    public static event Action<float> OnFoleyVolumeChanged;

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
        PlayMusic("MainMenu");
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

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void FoleyVolume(float volume)
    {
        foleySource.volume = volume;
        OnFoleyVolumeChanged?.Invoke(volume);
    }
}