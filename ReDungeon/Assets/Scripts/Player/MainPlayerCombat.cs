using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerCombat : MonoBehaviour
{
    public List<GameObject> playerPrefabs;
    public List<GameObject> playerUnits;
    // Start is called before the first frame update
    void Start()
    {
        playerUnits = new List<GameObject>();
        for(int i = 0; i < playerPrefabs.Count; i++)
        {
            playerUnits.Add(Instantiate(playerPrefabs[i],gameObject.transform));
            playerUnits[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
