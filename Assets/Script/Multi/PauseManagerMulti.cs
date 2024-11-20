using UnityEngine;
using TMPro; // Ajouter TextMesh Pro namespace
using UnityEngine.UI; // Ajouter pour Button d'Unity
using UnityEngine.SceneManagement; // Ajouter pour la gestion des scènes

public class PauseManagerMulti : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Menu de pause
    public Button resumeButton;     // Bouton pour reprendre
    public Button quitButton;       // Bouton pour quitter

    private void Start()
    {
        // Initialisation du menu de pause
        pauseMenuUI.SetActive(false);  // Le menu est initialement caché

        // Lier les boutons aux actions
        resumeButton.onClick.AddListener(ResumeGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        // Permet de montrer/masquer le menu de pause lorsqu'on appuie sur la touche "Échap"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                ResumeGame(); // Si le menu est déjà affiché, on le cache
            }
            else
            {
                ShowPauseMenu(); // Sinon, on l'affiche
            }
        }
    }

    // Affiche le menu de pause
    public void ShowPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Reprendre la partie, on cache simplement le menu sans toucher à Time.timeScale
    public void ResumeGame()
    {
        Debug.Log("ResumeGame");
        pauseMenuUI.SetActive(false);
    }

    // Quitter le jeu et revenir au menu principal
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        SceneManager.LoadScene("MainMenu");
    }
}
