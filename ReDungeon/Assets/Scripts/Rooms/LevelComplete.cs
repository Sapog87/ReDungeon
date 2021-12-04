using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public Canvas canvas;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            StopPlayer();
            canvas.gameObject.SetActive(true);
        }
    }
    private void StopPlayer()
    {
        GameObject.Find("Player").GetComponent<MainPlayerMovement>().StopPlayer();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
