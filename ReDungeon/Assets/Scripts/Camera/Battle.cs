using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        string playerTag = "Player";
        GameObject mainPlayer = GameObject.FindGameObjectWithTag(playerTag);
        MainPlayerMovement mpm = mainPlayer.GetComponent<MainPlayerMovement>();
        mpm.enabled = false;
        Application.LoadLevelAdditive("CombatScene");
    }
}
