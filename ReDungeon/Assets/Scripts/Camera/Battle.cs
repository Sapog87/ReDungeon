using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Battle : MonoBehaviour
{
    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            LoadNextScene();
    }

    [System.Obsolete]
    private void LoadNextScene()
    {
        Destroy(gameObject);

        GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerMovement>().enabled = false;

        GameObject.FindGameObjectWithTag("PlayerEventSystem").GetComponent<EventSystem>().enabled = false;

        Application.LoadLevelAdditive("CombatScene");
    }
}
