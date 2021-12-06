using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneLoader sceneLoader;

    public void NewRun()
    {
        // Здесь будет вызов окна выбора сложности нового забега
        sceneLoader.LoadScene("Generation"); // Загружаем сцену выбора классов персонажей 
    }

    public void ContinueRun()
    {
        // Здесь будет вызов считывания данных сохранения
        sceneLoader.LoadScene(""); // Загружаем сцену перемещения по карте с данными сохранения
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
