using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum BattleState { START, COMBAT, VICTORY, DEFEAT }

public class BattleManager : MonoBehaviour
{
    // public Room RoolLayout;
    BattleState State;

    public List<GameObject> AlliedPrefabs;
    public List<GameObject> EnemyPrefabs;

    public List<Transform> AlliedPositions;
    public List<Transform> EnemyPositions;

    List<Unit> AlliedUnits;
    List<Unit> EnemyUnits;

    // Start is called before the first frame update
    void Start()
    {
        State = BattleState.START;
        StartCoroutine(setupBattle());
    }

    IEnumerator setupBattle()
    {
        List<GameObject> AlliedGO = new List<GameObject>();
        AlliedUnits = new List<Unit>();
        for (int i = 0; i < AlliedPrefabs.Count; i++)
        {
            AlliedGO.Add(Instantiate(AlliedPrefabs[i], AlliedPositions[i]));
            AlliedUnits.Add(AlliedGO[i].GetComponent<Unit>());
            AlliedUnits[i].enemies = EnemyUnits;
            AlliedUnits[i].allies = AlliedUnits;
        }

        List<GameObject> EnemyGO = new List<GameObject>();
        EnemyUnits = new List<Unit>();
        for (int i = 0; i < EnemyPrefabs.Count; i++)
        {
            EnemyGO.Add(Instantiate(EnemyPrefabs[i], EnemyPositions[i]));
            EnemyUnits.Add(EnemyGO[i].GetComponent<Unit>());
            EnemyUnits[i].enemies = AlliedUnits;
            EnemyUnits[i].allies = EnemyUnits;
        }

        foreach(Unit a in AlliedUnits)
        {
            a.allies = AlliedUnits;
            a.enemies = EnemyUnits;
            a.Allied = true;
        }

        foreach (Unit a in EnemyUnits)
        {
            a.enemies = AlliedUnits;
            a.allies = EnemyUnits;
        }
        yield return new WaitForSeconds(4f);

        State = BattleState.COMBAT;
        NextTurn();
    }
    void NextTurn()
    {
        bool anyoneAlive = false;
        foreach(Unit allyunit in AlliedUnits)
        {
            anyoneAlive = anyoneAlive || !allyunit.isDead;
        }
        if (!anyoneAlive) { State = BattleState.DEFEAT; }
        else
        {
            anyoneAlive = false;
            foreach (Unit enemyunit in EnemyUnits)
            {
                anyoneAlive = anyoneAlive || !enemyunit.isDead;
            }
            if (!anyoneAlive) { State = BattleState.VICTORY; }
            else
            {
                Unit selectedUnit = null;
                foreach (Unit allyunit in AlliedUnits)
                {
                    if (!allyunit.isDead && (selectedUnit == null || allyunit.recoil < selectedUnit.recoil))
                        selectedUnit = allyunit;
                }

                foreach (Unit enemyunit in EnemyUnits)
                {
                    if (!enemyunit.isDead && (selectedUnit == null || enemyunit.recoil < selectedUnit.recoil))
                        selectedUnit = enemyunit;
                }

                foreach (Unit allyunit in AlliedUnits)
                {
                    allyunit.recoil -= selectedUnit.recoil;
                }
                foreach (Unit enemyunit in EnemyUnits)
                {
                    enemyunit.recoil -= selectedUnit.recoil;
                }

                Debug.Log(selectedUnit.actions.Count);
                Action selectedAction = selectedUnit.AI();
                if (selectedAction.TargetType == TargetType.MANY)
                {
                    selectedAction.Invoke(selectedUnit, selectedAction.GetValidTargets(selectedUnit));
                }
                else if (selectedAction.TargetType == TargetType.ONE)
                {
                    List<Unit> targets = selectedAction.GetValidTargets(selectedUnit);
                    selectedAction.Invoke(selectedUnit, targets[Random.Range(0, targets.Count)]);
                }
                if (State == BattleState.COMBAT) { NextTurn(); }
            }
        }
    }
}
