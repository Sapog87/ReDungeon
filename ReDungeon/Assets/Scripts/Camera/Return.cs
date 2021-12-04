using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Generation"));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("CombatScene"));
        string playerTag = "Player";
        GameObject mainPlayer = GameObject.FindGameObjectWithTag(playerTag);
        MainPlayerMovement mpm = mainPlayer.GetComponent<MainPlayerMovement>();
        mpm.enabled = true;
    }
}
