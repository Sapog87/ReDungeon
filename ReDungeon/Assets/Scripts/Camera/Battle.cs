using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    public List<SpriteRenderer> mobBoxes;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            LoadNextScene();
        }
        
    }

    private void Awake()
    {
        //Destroy(gameObject);   // enable to despawn all mobs
        LevelManager lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        lm.SetCurrentManager();

        if (mobBoxes.Count > 0)
        {
            foreach (SpriteRenderer sp in mobBoxes)
            {
                sp.sprite = lm.currentManager.unitprebuilds[Random.Range(0, lm.currentManager.unitprebuilds.Length)].sprites[0];
                if (Random.Range(0, 2) == 1)
                    sp.flipX = true;
            }
        }
    }


    private void LoadNextScene()
    {
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
        Destroy(gameObject);
    }
}
