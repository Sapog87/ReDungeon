using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerCombat : MonoBehaviour
{
    public List<Unit> playerUnitsPrefabs;
    public List<Unit> _playerUnits;
    public List<int> UnitXp;
    public List<int> UnitLvXp;
    public List<int> UnitLevel;

    public static MainPlayerCombat instance;

    void Awake()
    {
        recreateCharecters();
        DontDestroyOnLoad(gameObject);
        
        if (instance == null)
            instance = this;
        else
        {
            GameObject.FindGameObjectWithTag("Player").transform.SetPositionAndRotation(new Vector3(-16, 0, 0), new Quaternion());
            instance.recreateCharecters();
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void recreateCharecters()
    {
        _playerUnits = new List<Unit>();
        for (int i = 0; i < playerUnitsPrefabs.Count; i++)
        {
            _playerUnits.Add(Instantiate(playerUnitsPrefabs[i]));
        }
    }
}
