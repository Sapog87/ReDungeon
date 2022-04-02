using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public GameObject buttonPrefab;
    public Transform[] playerBasePositions = new Transform[3];
    public Transform[] opponentBasePositions = new Transform[3];
    public SpawnManager manager;
    UnitObject[] PlayerUnits = new UnitObject[5];
    UnitObject[] OpponentUnits = new UnitObject[5];
    UnitObject[] AvailablePUnits => PlayerUnits.Where(x => x != null && !x.unit.isDead).ToArray();
    UnitObject[] AvailableOUnits => OpponentUnits.Where(x => x != null && !x.unit.isDead).ToArray();
    public Transform buttonsBackGround;
    void Start()
    {
        SetupBattle();
        Battlestep();
    }

    private void SetupBattle()
    {
        Unit[] unitsO = manager.CreateUnits();
        for (int i = 0; i < unitsO.Length; i++)
        {
            GameObject UnitGO = Instantiate(unitPrefab, opponentBasePositions[i].transform);
            UnitObject unitObject = UnitGO.GetComponent<UnitObject>();
            unitObject.Setup(unitsO[i]);
            OpponentUnits[i] = unitObject;
        }

        Unit[] unitsP = FindObjectOfType<MainPlayerCombat>()._playerUnits.ToArray();
        for (int i = 0; i < unitsP.Length; i++)
        {
            GameObject UnitGO = Instantiate(unitPrefab, playerBasePositions[i].transform);
            UnitObject unitObject = UnitGO.GetComponent<UnitObject>();
            unitObject.Setup(unitsP[i]);
            PlayerUnits[i] = unitObject;
        }
    }
    /// <summary>
    /// This Function Manages Battle, fragile, DO NOT TOUCH, except the TODO part
    /// </summary>
    /// <returns></returns>
    private async Task Battlestep()
    {
        try
        {
            while (true)
            {
                if (AvailableOUnits.Length == 0)
                {
                    ConcludeBattle();
                    break;
                }
                if (AvailablePUnits.Length == 0)
                {
                    break;
                    //TODO change scene to main menu
                }
                UnitObject playerUnit = null;
                foreach (UnitObject unit in AvailablePUnits)
                {
                    if (!unit.unit.isDead && (playerUnit == null || playerUnit.recoil > unit.recoil))
                    {
                        playerUnit = unit;
                    }
                }
                UnitObject opponentUnit = null;
                foreach (UnitObject unit in AvailableOUnits)
                {
                    if (!unit.unit.isDead && (opponentUnit == null || opponentUnit.recoil > unit.recoil))
                        opponentUnit = unit;
                }
                if (opponentUnit.recoil < playerUnit.recoil)
                {
                    Debug.Log($"{opponentUnit.unit.name} is acting");
                    int recoil = opponentUnit.recoil;
                    foreach (UnitObject unit in AvailableOUnits)
                    {
                        unit.recoil -= recoil;
                    }
                    foreach (UnitObject unit in AvailablePUnits)
                    {
                        unit.recoil -= recoil;
                    }
                    Action selectedaction = opponentUnit.unit.Ai(AvailableOUnits, AvailablePUnits);
                    UnitObject[] selectTarget = selectedaction.GetTargets(opponentUnit, AvailableOUnits, AvailablePUnits);
                    foreach (UnitObject unit in selectTarget)
                    {
                        await selectedaction.DoAction(opponentUnit, unit);
                        unit.SetSprite(0);
                    }
                    opponentUnit.recoil += selectedaction.recoil;
                }
                else
                {
                    Debug.Log($"{playerUnit.unit.name} is acting");
                    int recoil = playerUnit.recoil;
                    foreach (UnitObject unit in AvailableOUnits)
                    {
                        unit.recoil -= recoil;
                    }
                    foreach (UnitObject unit in AvailablePUnits)
                    {
                        unit.recoil -= recoil;
                    }
                    //TODO link controlls
                    Action selectedaction = playerUnit.unit.Ai(AvailablePUnits, AvailableOUnits);
                    UnitObject[] selectTarget = selectedaction.GetTargets(opponentUnit, AvailablePUnits, AvailableOUnits);
                    foreach (UnitObject unit in selectTarget)
                    {
                        await selectedaction.DoAction(playerUnit, unit);
                        unit.SetSprite(0);
                    }
                    playerUnit.recoil += selectedaction.recoil;
                }
                await Task.Delay(100);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
    /// <summary>
    /// This function Concludes Battle
    /// </summary>
    private void ConcludeBattle()
    {
        Unit[] unitsP = FindObjectOfType<MainPlayerCombat>()._playerUnits.ToArray();
        for(int i = 0; i < unitsP.Length; i++)
        {
            unitsP[i] = PlayerUnits[i].unit;
        }
        //TODO change scene go "generation"
    }

    private void ResetField()
    {
        for (int i = 0; i < PlayerUnits.Length; i++)
        {
            PlayerUnits[i].transform.position = playerBasePositions[i].position;
            OpponentUnits[i].transform.position = opponentBasePositions[i].position;
        }
    }
}
