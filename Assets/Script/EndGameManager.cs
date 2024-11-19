using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
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
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame");
        SceneManager.LoadScene("MainMenu");
    }
}
