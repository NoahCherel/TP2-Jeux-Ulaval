using Unity.Netcode;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public void OnHostButtonClicked()
    {
        // Start the server and host the game
        NetworkManager.Singleton.StartHost();
        // Load the scene only for the server
        NetworkManager.Singleton.SceneManager.LoadScene("GameMulti", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void OnJoinButtonClicked()
    {
        // Start the client and connect to the server
        NetworkManager.Singleton.StartClient();
    }
}
