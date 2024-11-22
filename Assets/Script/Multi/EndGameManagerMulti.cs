using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class EndGameManagerMulti : MonoBehaviour
{
    public GameObject endMenuUI;
    public Button quitButton;

    private void Start()
    {
        endMenuUI.SetActive(false);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void ShowEndMenu()
    {
        endMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitGame()
    {
        if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsClient)
        {
            Debug.Log("Déconnexion du serveur...");
            NetworkManager.Singleton.Shutdown();
        }
        Debug.Log("QuitGame");
        SceneManager.LoadScene("MainMenu");
    }
}
