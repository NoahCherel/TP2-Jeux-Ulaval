using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    public GameObject optionMenu; // Le Canvas contenant les éléments UI globaux

    private void Awake()
    {
        // Assurez-vous qu'il n'y ait qu'une seule instance du GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Conserve ce GameObject à travers les scènes
        }
        else
        {
            Destroy(gameObject); // Détruit les GameManagers en trop
            return;
        }
    }

    private void Start()
    {
        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        // Chargez la scène principale après l'initialisation
        SceneManager.LoadScene("MainMenu");
    }

    public void ActivateOptionMenu()
    {
        if (optionMenu != null)
        {
            optionMenu.SetActive(true);
        }
    }
}
