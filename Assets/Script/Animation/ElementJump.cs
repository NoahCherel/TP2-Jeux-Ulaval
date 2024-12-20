using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementJump : MonoBehaviour
{
    public GameObject newObject; // Le nouveau GameObject à afficher
    public GameObject currentObject; // L'objet actuel
    public float duration = 1f; // Durée de l'animation
    private List<Vector3> initialPositions = new List<Vector3>(); // Liste pour stocker les positions initiales

    public void StartJumpAnimation()
    {
        LeanTween.moveLocalY(gameObject, 150, 1f).setEaseOutQuart().setLoopPingPong();
    }

    public void StartLeftAnimation()
    {
        LeanTween.moveLocalX(gameObject, -1000, 1f).setEaseOutQuart();
    }

    public void StartRightAnimation()
    {
        LeanTween.moveLocalX(gameObject, 1000, 1f).setEaseOutQuart();
    }

    public void ApplyAlternatingAnimations()
    {
        RectTransform[] rectTransforms = transform.GetComponentsInChildren<RectTransform>(true);

        List<RectTransform> filteredRects = new List<RectTransform>();
        foreach (RectTransform rect in rectTransforms)
        {
            if (rect.transform.parent == transform)
            {
                filteredRects.Add(rect);
            }
        }

        initialPositions.Clear();
        foreach (RectTransform rect in filteredRects)
        {
            initialPositions.Add(rect.localPosition);
        }

        for (int i = 0; i < filteredRects.Count; i++)
        {
            if (i % 2 == 0)
            {
                LeanTween.moveLocalX(filteredRects[i].gameObject, -1100, duration).setEaseOutQuart();
            }
            else
            {
                LeanTween.moveLocalX(filteredRects[i].gameObject, 1100, duration).setEaseOutQuart();
            }
        }
    }

    public void SwitchObjectAfterAnimation()
    {
        ApplyAlternatingAnimations();

        Invoke(nameof(ResetPositionsAndChangeObject), duration);
    }

    private void ResetPositionsAndChangeObject()
    {
        RectTransform[] rectTransforms = transform.GetComponentsInChildren<RectTransform>(true);

        List<RectTransform> filteredRects = new List<RectTransform>();
        foreach (RectTransform rect in rectTransforms)
        {
            if (rect.transform.parent == transform)
            {
                filteredRects.Add(rect);
            }
        }

        for (int i = 0; i < filteredRects.Count; i++)
        {
            filteredRects[i].localPosition = initialPositions[i];
        }

        currentObject.SetActive(false);
        newObject.SetActive(true);
    }
}
