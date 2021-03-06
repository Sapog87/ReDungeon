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
        {
            LoadNextScene();
        }
            
    }

    [System.Obsolete]
    private void LoadNextScene()
    {
        Destroy(gameObject);

        GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerMovement>().enabled = false;

        GameObject.FindGameObjectWithTag("PlayerEventSystem").GetComponent<EventSystem>().enabled = false;

        GameObject.FindGameObjectWithTag("MiniMap").GetComponent<Canvas>().enabled = false;

        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothTrackChange(
                                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().GetPeacefulSoundtrackName(),
                                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().GetCombatSoundtrackName(),
                                0.5f, -1);

        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().SetCurrentManager();
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().isBossBattle = false;

        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadScene_Special("CombatScene");
    }
}
