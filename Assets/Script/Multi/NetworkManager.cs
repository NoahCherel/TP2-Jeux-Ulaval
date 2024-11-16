using UnityEngine;
using Unity.Netcode;

public class NetworkManagerSingleton : MonoBehaviour
{
    private void Awake()
    {
        // Gérer le singleton du NetworkManager
        if (NetworkManager.Singleton != null && NetworkManager.Singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
