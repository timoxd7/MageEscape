using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject progressIndicator;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        if (progressIndicator != null)
            progressIndicator.SetActive(true);

        // Wait for Scene
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}

