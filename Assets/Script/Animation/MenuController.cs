using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;

    public void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(1, 0.5f);

        box.localScale = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void CloseDialog()
    {
        background.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }
}
