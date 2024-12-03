using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OptionMenuController : MonoBehaviour
{
    public static OptionMenuController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
