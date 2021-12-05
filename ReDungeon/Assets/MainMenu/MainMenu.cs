using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public void NewRun()
    {
        // ����� ����� ����� ���� ������ ��������� ������ ������
        sceneLoader.LoadScene("Generation"); // ��������� ����� ������ ������� ���������� 
    }

    public void ContinueRun()
    {
        // ����� ����� ����� ���������� ������ ����������
        sceneLoader.LoadScene(""); // ��������� ����� ����������� �� ����� � ������� ����������
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
