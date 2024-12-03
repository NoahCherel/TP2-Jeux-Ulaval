using UnityEngine;
using UnityEngine.UI; // Pour interagir avec le bouton

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] private Button Button;

    private void Start()
    {
        if (Button != null)
        {
            Button.onClick.AddListener(OnButtonClick);

        }
        else
        {
            Debug.LogError("Button not assigned in the Inspector!");
        }
    }

    private void OnButtonClick()
    {
        // Vérifie si GameManager est chargé
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ActivateOptionMenu();
        }
        else
        {
            Debug.LogError("GameManager is not loaded yet!");
        }
    }

    public void OnButtonClickPlaySFX(string soundName)
    {
        if (SoundManager.instance != null && !string.IsNullOrEmpty(soundName))
        {
            SoundManager.instance.PlaySFX(soundName);
        }
    }

    public void OnButtonClickPlayMusic(string soundName)
    {
        if (SoundManager.instance != null && !string.IsNullOrEmpty(soundName))
        {
            SoundManager.instance.PlayMusic(soundName);
        }
    }

    public void OnButtonClickPlayFoley(string soundName)
    {
        if (SoundManager.instance != null && !string.IsNullOrEmpty(soundName))
        {
            SoundManager.instance.PlayFoley(soundName);
        }
    }
}
