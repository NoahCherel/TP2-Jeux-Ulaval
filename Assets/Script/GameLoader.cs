using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLoader : MonoBehaviour
{

    public GameObject LoadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    public void LoadScene(int indexScene)
    {
        StartCoroutine(LoadAsync(indexScene));
    }

    IEnumerator LoadAsync(int indexScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(indexScene);

        LoadingScreen.SetActive(true);

        while (!operation.isDone) {

            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            progressText.text = progress * 100 + "%";

            yield return null;
        }
    }
}
