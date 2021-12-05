using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool pause = false;
    public GameObject panel;

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pause)
            {
                Time.timeScale = 0;
                pause = true;
                panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pause = false;
                panel.SetActive(false);
            }
        }
    }

    public void _pause()
    {
        Time.timeScale = 1;
        pause = false;
        panel.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
