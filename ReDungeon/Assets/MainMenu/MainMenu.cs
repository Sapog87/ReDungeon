using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public AudioManager audioManager;
    private void Start()
    {
        audioManager.PlayMusic("Theme", 0.5f, 0);
    }

    public void NewRun()
    {
        // ����� ����� ����� ���� ������ ��������� ������ ������
        sceneLoader.LoadScene("Generation");
    }

    /*
    public void ContinueRun()
    {
        
        
        sceneLoader.LoadScene(""); // ��������� ����� ����������� �� ����� � ������� ����������
    }
    */

    public void QuitGame()
    {
        //audioManager.SmoothTrackChange("Peaceful", "Combat", 0.5f, 0);
        Application.Quit();
    }

    private void OnDestroy()
    {
        //audioManager.SmoothTrackFade("Theme");
        audioManager.SmoothTrackChange("Theme", "Peaceful", 0.5f, 0);
    }

}
