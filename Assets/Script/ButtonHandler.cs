using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
