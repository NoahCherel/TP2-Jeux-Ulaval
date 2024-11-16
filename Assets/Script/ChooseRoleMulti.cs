using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class ChooseRoleMulti : MonoBehaviour
{
    [SerializeField] private Button HostButton;
    [SerializeField] private Button clientButton;

    private void Awake()
    {
        HostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            HostButton.gameObject.SetActive(false);
            clientButton.gameObject.SetActive(false);
        });
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            HostButton.gameObject.SetActive(false);
            clientButton.gameObject.SetActive(false);
        });
    }
}
