using UnityEngine;

public class MobSoundHandler : MonoBehaviour
{
    public string foleySoundName;

    public void PlayFoleySound()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayFoley(foleySoundName);
        }
        else
        {
            Debug.LogWarning("SoundManager instance is missing!");
        }
    }
}
