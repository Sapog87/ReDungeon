using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private Canvas completionMessage, suggestionMessage;
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
            if (GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().level == 5)
            {
                completionMessage.gameObject.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("Generation");
            }
        }
    }
    public void BackToMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        Destroy(GameObject.Find("AudioManager"));
        SceneManager.LoadScene("MainMenu");
    }
}
