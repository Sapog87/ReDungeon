using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerMovement : MonoBehaviour
{
    Animator animator;

    [Header("Parameters")]
    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private string playerTag;
    [SerializeField] private float speed;


    void Awake()
    {
        playerTag = "Player";
        mainPlayer = GameObject.FindGameObjectWithTag(playerTag);
        speed = 10f;

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Time.fixedDeltaTime = 0.002f;

        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");

        mainPlayer.transform.Translate(Vector2.up * speed * yPos * Time.fixedDeltaTime);
        mainPlayer.transform.Translate(Vector2.right * speed * xPos * Time.fixedDeltaTime);

        animator.SetFloat("Horizontal", xPos);
        animator.SetFloat("Vertical", yPos);
        animator.SetBool("IsMoving", xPos != 0 || yPos != 0);
    }
    public void StopPlayer()
    {
        speed = 0;
    }
}
