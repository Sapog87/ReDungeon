using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Header("Parameters")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private string playerTag;
    [SerializeField] private float speed;
    [SerializeField] private float offsetXY;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        playerTag = "Player";
        playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        speed = 12f;
        offsetXY = 1.5f;

        this.transform.position = new Vector3()
        {
            x = playerTransform.position.x,
            y = playerTransform.position.y,
            z = playerTransform.position.z - 10,
        };
    }
    
    void FixedUpdate()
    {
        Time.fixedDeltaTime = 0.002f;

        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");

        Vector3 offset = new Vector3(offsetXY * xPos, offsetXY * yPos);

        Vector3 target = new Vector3()
        {
            x = playerTransform.position.x,
            y = playerTransform.position.y,
            z = playerTransform.position.z - 10,
        };

        this.transform.position = Vector3.Lerp(this.transform.position, target+offset, speed * Time.fixedDeltaTime);
    }
}
