using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothTrackUnfade("Theme", 0.5f, 0);
    }

    public void NewRun()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothTrackChange("Theme", "Peaceful", 0.5f, 0);
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadScene_NoLoadingScreen("Generation");
    }

    /*
    public void ContinueRun()
    {
        sceneLoader.LoadScene(""); // Загружаем сцену перемещения по карте с данными сохранения
    }
    */

    public void QuitGame()
    {
        Application.Quit();
    }

}
