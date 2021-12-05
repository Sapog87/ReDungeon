using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Generation"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("CombatScene"));

        GameObject.FindGameObjectWithTag("PlayerEventSystem").GetComponent<EventSystem>().enabled = true;

        GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerMovement>().enabled = true;
    }
}
