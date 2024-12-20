using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    public void OpenWindow()
    {
        transform.LeanScale(Vector3.one, 0.8f);
    }

    public void CloseWindow()
    {
        transform.LeanScale(Vector3.zero, 1f).setEaseInBack();
    }
}
