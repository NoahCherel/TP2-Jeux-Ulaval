using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode; // Ajouter pour utiliser NetworkManager

public class PauseMenuManagerMulti : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button quitButton;
    private void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                ShowPauseMenu();
            }
        }
    }

    public void ShowPauseMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame");
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        // Si le joueur est connecté à un serveur ou un hôte
        if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsClient)
        {
            Debug.Log("Déconnexion du serveur...");
            NetworkManager.Singleton.Shutdown();
        }

        // Charger le menu principal
        SceneManager.LoadScene("MainMenu");
    }
}
