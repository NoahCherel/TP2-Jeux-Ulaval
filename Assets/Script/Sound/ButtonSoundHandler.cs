using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundHandler : MonoBehaviour
{
    public enum ButtonSoundType { Default, Next, Back }
    public ButtonSoundType soundType;

    public void PlaySound()
    {
        AudioClip clip = null;
        switch (soundType)
        {
            case ButtonSoundType.Next:
                clip = SoundManager.instance.nextSound;
                break;
            case ButtonSoundType.Back:
                clip = SoundManager.instance.backSound;
                break;
            default:
                clip = SoundManager.instance.FirstClickSound;
                break;
        }
        if (clip != null)
        {
            SoundManager.instance.PlaySound(clip);
        }
    }
}
