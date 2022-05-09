using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossBattle : MonoBehaviour
{
    public List<SpriteRenderer> mobBoxes;
    public LevelComplete lc;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            LoadNextScene();
        }

    }

    private void Awake()
    {
        LevelManager lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        lm.SetCurrentManager_Boss();

        if (mobBoxes.Count > 0)
        {
            for(int i = 0; i < mobBoxes.Count; i++)
                mobBoxes[i].sprite = lm.currentManager.unitprebuilds[i].sprites[0];
                
        }
        lc.gameObject.SetActive(false);
    }

    private void LoadNextScene()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerMovement>().enabled = false;

        GameObject.FindGameObjectWithTag("PlayerEventSystem").GetComponent<EventSystem>().enabled = false;

        GameObject.FindGameObjectWithTag("MiniMap").GetComponent<Canvas>().enabled = false;

        GameObject.FindGameObjectWithTag("PauseGeneration").GetComponent<Pause>().enabled = false;

        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothFadeAllTrackChange(
                                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().GetBossCombatSoundtrackName(),
                                0.5f, -1);

        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().SetCurrentManager_Boss();

        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadScene_Special("CombatScene");
        lc.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
