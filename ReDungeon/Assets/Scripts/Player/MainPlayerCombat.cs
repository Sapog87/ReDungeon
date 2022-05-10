using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerCombat : MonoBehaviour
{
    public List<Unit> playerUnitsPrefabs;
    public List<Unit> _playerUnits;
    public List<int> UnitXp = new List<int>();
    public List<int> UnitLvXp = new List<int>();

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
            _playerUnits.Add(Unit.Create(playerUnitsPrefabs[i],0));
            UnitXp.Add(0);
            UnitLvXp.Add(4);
        }
    }
}
