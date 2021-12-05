using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    private Canvas completionMessage, suggestionMessage;
    private void Awake()
    {
        completionMessage = gameObject.transform.Find("CompletionMessage").GetComponent<Canvas>();
        suggestionMessage = gameObject.transform.Find("SuggestionMessage").GetComponent<Canvas>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            suggestionMessage.gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            suggestionMessage.gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            StopPlayer();
            completionMessage.gameObject.SetActive(true);
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
