using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossBattle : MonoBehaviour
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
                                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().GetBossCombatSoundtrackName(),
                                0.5f, -1);

        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().SetCurrentManager_Boss();
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().isBossBattle = true;

        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadScene_Special("CombatScene");
    }
}
