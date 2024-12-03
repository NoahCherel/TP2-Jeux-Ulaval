using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider musicSlider, sfxSlider, foleySlider;

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
