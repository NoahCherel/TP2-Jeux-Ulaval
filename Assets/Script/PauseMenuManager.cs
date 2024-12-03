using UnityEngine;
using TMPro; // Ajouter TextMesh Pro namespace
using UnityEngine.UI; // Ajouter pour Button d'Unity
using UnityEngine.SceneManagement; // Ajouter pour la gestion des scènes

public class PauseMenuManager : MonoBehaviour
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
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        SoundManager.instance.PlayMusic("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}
