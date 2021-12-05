using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Defeat : MonoBehaviour
{
    public GameObject panel;
    void Start()
    {
        //CombatEventSystem
        //Where(x => x.tag == "ESC").ToArray()[0].GetComponent<Pause>().enabled = false;
        SceneManager.GetSceneByName("CombatScene").GetRootGameObjects().Where(x => x.tag == "MainCamera").First().GetComponentInChildren<Pause>().enabled = false;
        Time.timeScale = 0;
        panel.SetActive(true);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
