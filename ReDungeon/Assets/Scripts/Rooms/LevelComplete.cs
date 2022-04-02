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
        if (suggestionMessage.gameObject.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            if (GameObject.FindGameObjectWithTag("Level").GetComponent<Level>().level == 5)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerMovement>().enabled = false;
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
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("Level"));
        Destroy(GameObject.Find("AudioManager"));
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadScene("MainMenu");
    }
}
