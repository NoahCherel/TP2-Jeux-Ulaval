using UnityEngine;
using TMPro; // Ajouter TextMesh Pro namespace
using UnityEngine.UI; // Ajouter pour Button d'Unity
using UnityEngine.SceneManagement; // Ajouter pour la gestion des scènes

public class EndGameManager : MonoBehaviour
{
    public GameObject endMenuUI;  // UI du menu de fin de jeu
    public Button quitButton;     // Bouton Quitter

    private void Start()
    {
        endMenuUI.SetActive(false);  // Masque le menu de fin de jeu au démarrage
        quitButton.onClick.AddListener(QuitGame);  // Lier le bouton Quitter à la fonction QuitGame
    }

    // Appelée quand la partie se termine
    public void ShowEndMenu()
    {
        Time.timeScale = 0f;         // Arrête le temps
        endMenuUI.SetActive(true);   // Affiche l'UI de fin de jeu
        Cursor.visible = true;      // Rend le curseur visible
        Cursor.lockState = CursorLockMode.None; // Déverrouille le curseur
    }

    // Fonction pour quitter le jeu
    public void QuitGame()
    {
        Debug.Log("QuitGame");
        SoundManager.instance.PlaySpacialMusic("MainMenu");
        // SoundManager.instance.StopMusic("GameOver");
        SceneManager.LoadScene("MainMenu");  // Charge la scène "MainMenu", ou une autre scène que vous souhaitez
    }
}
