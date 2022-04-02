using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int level;

    private void Awake()
    {
        level = 0;
        DontDestroyOnLoad(gameObject);
    }
}
