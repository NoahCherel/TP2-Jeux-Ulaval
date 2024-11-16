using Unity.Netcode;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public void OnHostButtonClicked()
    {
        // Load the scene first
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameMulti", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}