using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Slider progressBar;
    public Animator transitionAnimator;
    public float animationTime;
    public string transitionTrigger;
    
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Wait for Animation
        yield return new WaitForSeconds(animationTime);

        // Wait for Scene
        while (!operation.isDone)
        {
            progressBar.gameObject.SetActive(true);
            progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }
    }
}
