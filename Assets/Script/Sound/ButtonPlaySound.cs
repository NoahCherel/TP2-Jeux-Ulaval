using UnityEngine;
using UnityEngine.UI;

public class ButtonPlaySound : MonoBehaviour
{
    [SerializeField] public bool isSpatialSound = false;

    public void OnButtonClickPlaySFX(string soundName)
    {
        if (SoundManager.instance != null && !string.IsNullOrEmpty(soundName) && !isSpatialSound)
        {
            SoundManager.instance.PlaySFX(soundName);
        } else if (SoundManager.instance != null && !string.IsNullOrEmpty(soundName) && isSpatialSound)
        {
            SoundManager.instance.PlaySpacialSFX(soundName);
        }
    }

    public void OnButtonClickPlayMusic(string soundName)
    {
        if (SoundManager.instance != null && !string.IsNullOrEmpty(soundName) && !isSpatialSound)
        {
            SoundManager.instance.PlayMusic(soundName);
        } else if (SoundManager.instance != null && !string.IsNullOrEmpty(soundName) && isSpatialSound)
        {
            SoundManager.instance.PlaySpacialMusic(soundName);
        }
    }

    public void OnButtonClickPlayFoley(string soundName)
    {
        if (SoundManager.instance != null && !string.IsNullOrEmpty(soundName) && !isSpatialSound)
        {
            SoundManager.instance.PlayFoley(soundName);
        } else if (SoundManager.instance != null && !string.IsNullOrEmpty(soundName) && isSpatialSound)
        {
            SoundManager.instance.PlaySpacialFoley(soundName);
        }
    }
}
