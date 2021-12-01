using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    // public Room RoolLayout;

    public List<GameObject> AlliedPrefabs;
    public List<GameObject> EnemyPrefabs;

    public List<Transform> AlliedPositions;
    public List<Transform> EnemyPositions;

    List<Unit> AlliedUnits;
    List<Unit> EnemyUnits;

    // Start is called before the first frame update
    void Start()
    {
        setupBattle();
    }

    void setupBattle()
    {
        List<GameObject> AlliedGO = new List<GameObject>();
        AlliedUnits = new List<Unit>();
        for (int i = 0; i < AlliedPrefabs.Count; i++)
        {
            AlliedGO.Add(Instantiate(AlliedPrefabs[i], AlliedPositions[i]));
            AlliedUnits.Add(AlliedGO[i].GetComponent<Unit>());
        }

        List<GameObject> EnemyGO = new List<GameObject>();
        EnemyUnits = new List<Unit>();
        for (int i = 0; i < EnemyPrefabs.Count; i++)
        {
            EnemyGO.Add(Instantiate(EnemyPrefabs[i], EnemyPositions[i]));
            EnemyUnits.Add(EnemyGO[i].GetComponent<Unit>());
        }
    }

}
