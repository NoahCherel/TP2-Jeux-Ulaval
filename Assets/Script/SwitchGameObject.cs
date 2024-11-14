using UnityEngine;

public class SwitchGameObject : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject objectToEnable;

    public void SwitchObjects()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }

        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }
}
