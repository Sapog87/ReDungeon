using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneLoader sceneLoader;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothTrackUnfade("Theme", 0.5f, 0);
    }

    private void Start()
    {
        
    }

    public void NewRun()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothTrackChange("Theme", "Peaceful", 0.5f, 0);
        sceneLoader.LoadScene("Generation");
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
