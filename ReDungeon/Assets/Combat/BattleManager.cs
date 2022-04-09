using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// is a state in which battle is
/// </summary>
public enum BattleState
{
    Setup,BattleStep,ActionSelection,TargetSelection, Conclusion
}
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
    public BattleState state = new BattleState();
    public Action selectedaction;
    void Start()
    {
        SetupBattle();
        Battlestep();
    }

    private void SetupBattle()
    {
        state = BattleState.Setup;
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
        await Task.Delay(1000);
        try
        {
            while (true)
            {
                state = BattleState.BattleStep;
                if (AvailableOUnits.Length == 0)
                {
                    ConcludeBattle();
                    break;
                }
                if (AvailablePUnits.Length == 0)
                {
                    GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothFadeAllTracks();
                    GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadScene_NoLoadingScreen("MainMenu");
                    SceneManager.UnloadSceneAsync("Generation");
                    break;
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
                    selectedaction = opponentUnit.unit.Ai(AvailableOUnits, AvailablePUnits);
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
                    state = BattleState.ActionSelection;
                    List<GameObject> buttons = new List<GameObject>();
                    for(int i = 0; i < playerUnit.unit.Actions.Count; i++)
                    {
                        buttons.Add(Instantiate(buttonPrefab,buttonsBackGround));
                        buttons.Last().transform.localPosition = new Vector3(35+i*60, 10, 0);
                        buttons.Last().GetComponent<ActionSelectButton>().Setup(playerUnit.unit.Actions[i], this);
                    }
                    selectedaction = null;
                    while (selectedaction == null)
                    {
                        await Task.Yield();
                    }
                    state = BattleState.BattleStep;
                    foreach(GameObject child in buttons)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                    Debug.Log(selectedaction.name);
                    UnitObject[] selectTarget = selectedaction.GetTargets(playerUnit, AvailablePUnits, AvailableOUnits);
                    foreach (UnitObject unit in selectTarget)
                    {
                        await selectedaction.DoAction(playerUnit, unit);
                        unit.SetSprite(0);
                    }
                    playerUnit.recoil += selectedaction.recoil;
                }
                await Task.Delay(10);
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
        for(int i = 0; i < PlayerUnits.Length; i++)
        {
            if (PlayerUnits[i] != null)
            {
                while(PlayerUnits[i].statuses.Count > 0)
                {
                    
                    Debug.Log(PlayerUnits[i].name);
                    PlayerUnits[i].statuses[0].RemoveStatus(PlayerUnits[i]);
                }
                unitsP[i] = PlayerUnits[i].unit;
            }
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerMovement>().enabled = true;

        GameObject.FindGameObjectWithTag("PlayerEventSystem").GetComponent<EventSystem>().enabled = true;

        GameObject.FindGameObjectWithTag("MiniMap").GetComponent<Canvas>().enabled = true;

        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothTrackChange("Combat", "Peaceful", 0.8f, -1);
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().UnloadScene_Special("CombatScene");
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
