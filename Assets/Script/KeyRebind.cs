using UnityEngine;
using TMPro;
using System.Collections;

public class KeyRebindButton : MonoBehaviour
{
    public string keyName; // Nom de la clé à remapper (ex : "MoveUpKey")
    public TMP_Text buttonText; // Référence au texte du bouton avec TMP pour afficher la touche sélectionnée

    private bool isWaitingForKey = false;

    void Start()
    {
        // Initialise le texte du bouton avec la touche actuelle configurée
        buttonText.text = InputManager.GetKeyCodeForKey(keyName).ToString();
    }

    public void OnButtonClick()
    {
        if (!isWaitingForKey)
        {
            StartCoroutine(WaitForKeyPress());
        }
    }

    private IEnumerator WaitForKeyPress()
    {
        isWaitingForKey = true;
        buttonText.text = "Press any key...";

        // Attend que l'utilisateur appuie sur une touche
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        // Récupère la touche appuyée
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                // Sauvegarde la touche sélectionnée dans InputManager
                InputManager.SetKey(keyName, key);
                buttonText.text = key.ToString(); // Met à jour le texte du bouton avec la nouvelle touche
                break;
            }
        }

        isWaitingForKey = false;
    }
}
