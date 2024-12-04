using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance; // Instance statique

    public Slider musicSlider, sfxSlider, foleySlider;

    private void Awake()
    {
        // Assurer que l'instance est correctement assignée
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Garder l'objet même quand la scène change
        }
        else
        {
            Destroy(gameObject); // S'assurer qu'il n'y a qu'une seule instance
        }
    }

    // Méthode pour mettre à jour les sliders avec les valeurs actuelles des volumes
    public void UpdateSliders(float musicVolume, float sfxVolume, float foleyVolume)
    {
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
        foleySlider.value = foleyVolume;
    }

    public void MusicVolume()
    {
        SoundManager.instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        SoundManager.instance.SFXVolume(sfxSlider.value);
    }

    public void FoleyVolume()
    {
        SoundManager.instance.FoleyVolume(foleySlider.value);
    }
}
