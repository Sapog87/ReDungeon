using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerMovement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private string playerTag;
    [SerializeField] private float speed;
    

    void Awake()
    {
        playerTag = "Player";
        mainPlayer = GameObject.FindGameObjectWithTag(playerTag);
        speed = 10f;

        DontDestroyOnLoad(this.gameObject);
    }

    void FixedUpdate()
    {
        Time.fixedDeltaTime = 0.002f;

        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");

        mainPlayer.transform.Translate(Vector2.up * speed * yPos * Time.fixedDeltaTime);
        mainPlayer.transform.Translate(Vector2.right * speed * xPos * Time.fixedDeltaTime);
    }
}
