using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject ui;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ui.SetActive(true);
        Destroy(gameObject);
    }
}
