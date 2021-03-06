using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    public Animator transition;
    const float transitionTime = 0.5f;

    public static SceneLoader instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    public void LoadScene_NoLoadingScreen(string sceneName)
    {
        StartCoroutine(LoadAsynchronously_NoLoadingScreen(sceneName));
    }

    public void LoadScene_Special(string sceneName)
    {
        StartCoroutine(LoadAsynchronously_Special(sceneName));
    }

    public void UnloadScene_Special(string sceneName)
    {
        StartCoroutine(UnloadAsynchronously_Special(sceneName));
    }

    IEnumerator LoadAsynchronously (string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }

        transition.SetTrigger("End");

        loadingScreen.SetActive(false);

        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator LoadAsynchronously_NoLoadingScreen(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);


        while (!operation.isDone)
        {
            yield return null;
        }

        transition.SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator LoadAsynchronously_Special(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive); //Application.LoadLevelAdditiveAsync(sceneName); 

        while (!operation.isDone)
        {
            yield return null;
        }

        transition.SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);
    }

    IEnumerator UnloadAsynchronously_Special(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }

        transition.SetTrigger("End");

        yield return new WaitForSeconds(transitionTime);
    }

}
