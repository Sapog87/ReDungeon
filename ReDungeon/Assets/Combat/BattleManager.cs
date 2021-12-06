using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public enum BattleState { START, COMBAT, VICTORY, DEFEAT }

public class BattleManager : MonoBehaviour
{
    // public Room RoolLayout;
    public BattleState State;
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
        List<GameObject> AlliedGO;
        AlliedUnits = new List<Unit>();
        AlliedGO = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerCombat>().playerUnits;
        for (int i = 0; i < AlliedGO.Count; i++)
        {
            AlliedGO[i].transform.SetParent(AlliedPositions[i]);
            AlliedGO[i].transform.localPosition = new Vector3(0, 0, 0);
            AlliedGO[i].transform.localScale = new Vector3(1, 1, 1);
            AlliedGO[i].SetActive(true);
            AlliedUnits.Add(AlliedGO[i].GetComponent<Unit>());
            AlliedGO[i].transform.parent.GetComponentInChildren<UnitHUDScript>().UpdateHP(AlliedUnits[i]);
            AlliedUnits[i].enemies = EnemyUnits;
            AlliedUnits[i].allies = AlliedUnits;
        }

        List<GameObject> EnemyGO = new List<GameObject>();
        EnemyUnits = new List<Unit>();
        for (int i = 0; i < EnemyPrefabs.Count; i++)
        {
            EnemyGO.Add(Instantiate(EnemyPrefabs[i], EnemyPositions[i]));
            EnemyUnits.Add(EnemyGO[i].GetComponent<Unit>());
            EnemyGO[i].transform.parent.GetComponentInChildren<UnitHUDScript>().UpdateHP(EnemyUnits[i]);
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
        StartCoroutine(NextTurn());
    }
    IEnumerator NextTurn()
    {
        bool anyoneAlive = false;
        foreach(Unit allyunit in AlliedUnits)
        {
            anyoneAlive = anyoneAlive || !allyunit.isDead;
        }
        if (!anyoneAlive) 
        {
            State = BattleState.DEFEAT;
        }
        else
        {
            anyoneAlive = false;
            foreach (Unit enemyunit in EnemyUnits)
            {
                anyoneAlive = anyoneAlive || !enemyunit.isDead;
            }
            if (!anyoneAlive) { State = BattleState.VICTORY;

                foreach(Unit a in AlliedUnits)
                {
                    foreach (Action b in a.actions) 
                    {
                        if (b.CDType == CDType.turn)
                            b.ResetCooldown();
                        if (b.CDType == CDType.fight)
                            b.Cooldown--;
                    }
                    a.recoil = 0;
                    a.gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
                }

                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Generation"));
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("CombatScene"));

                GameObject.FindGameObjectWithTag("PlayerEventSystem").GetComponent<EventSystem>().enabled = true;

                GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerMovement>().enabled = true;
            }
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

                int recoil = selectedUnit.recoil;
                foreach (Unit allyunit in AlliedUnits)
                {
                    allyunit.recoil -= recoil;
                }
                foreach (Unit enemyunit in EnemyUnits)
                {
                    enemyunit.recoil -= recoil;
                }

                foreach (Action act in selectedUnit.actions)
                {
                    if ((act.Cooldown > 0) && (act.CDType == CDType.turn))
                    {
                        act.Cooldown--;
                    }
                    if (act.Cooldown <= 0)
                    {
                        act.ResetCooldown();
                    }
                }

                Debug.Log(selectedUnit.actions.Count);
                Action selectedAction = selectedUnit.AI();
                if (selectedAction.TargetType == TargetType.MANY)
                {
                    yield return StartCoroutine(selectedAction.Invoke(selectedUnit, selectedAction.GetValidTargets(selectedUnit)));
                }
                else if (selectedAction.TargetType == TargetType.ONE)
                {
                    List<Unit> targets = selectedAction.GetValidTargets(selectedUnit);
                    yield return StartCoroutine(selectedAction.Invoke(selectedUnit, targets[Random.Range(0, targets.Count)]));
                }
                if (State == BattleState.COMBAT) { StartCoroutine(NextTurn()); }
            }
        }
    }
}
