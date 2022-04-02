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
    void Awake()
    {
        for (int i = 0; i<playerUnitsPrefabs.Count; i++)
        {
            _playerUnits.Add(Instantiate(playerUnitsPrefabs[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
