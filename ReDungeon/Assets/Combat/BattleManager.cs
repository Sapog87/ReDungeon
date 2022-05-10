using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject PointerPrefab;
    public Transform[] playerBasePositions = new Transform[3];
    public Transform[] opponentBasePositions = new Transform[3];
    public SpawnManager manager;
    public TextMeshProUGUI textbox;
    public UnitObject[] PlayerUnits = new UnitObject[5];
    public UnitObject[] OpponentUnits = new UnitObject[5];
    public UnitObject[] AvailablePUnits => PlayerUnits.Where(x => x != null).ToArray();
    public UnitObject[] AvailableOUnits => OpponentUnits.Where(x => x != null).ToArray();
    public Transform buttonsBackGround;
    [field: HideInInspector]
    public BattleState state = new BattleState();
    public Action selectedaction;
    [field: HideInInspector]
    public UnitObject[] PossibleTargets;
    [field: HideInInspector]
    public List<UnitObject> targetedUnits;
    [field: HideInInspector]
    public int maxU;

    async void Start()
    {
        await SetupBattle();
        Battlestep();
    }

    async private Task SetupBattle()
    {
        while (manager == null)
            await Task.Yield();
        state = BattleState.Setup;
        Unit[] unitsO = manager.CreateUnits();
        for (int i = 0; i < unitsO.Length; i++)
        {
            GameObject UnitGO = Instantiate(unitPrefab, opponentBasePositions[i].transform);
            UnitObject unitObject = UnitGO.GetComponent<UnitObject>();
            unitObject.sprite.flipX = true;
            unitObject.Setup(unitsO[i],OpponentUnits.Where(x=>x != null), PlayerUnits.Where(x=>x != null), this);
            OpponentUnits[i] = unitObject;
        }

        Unit[] unitsP = FindObjectOfType<MainPlayerCombat>()._playerUnits.ToArray();
        for (int i = 0; i < unitsP.Length; i++)
        {
            GameObject UnitGO = Instantiate(unitPrefab, playerBasePositions[i].transform);
            UnitObject unitObject = UnitGO.GetComponent<UnitObject>();
            unitObject.Setup(unitsP[i], PlayerUnits.Where(x => x != null), OpponentUnits.Where(x => x != null), this);
            PlayerUnits[i] = unitObject;
        }

        foreach (UnitObject unit in UnitObject.FilterAlive(AvailableOUnits))
            unit.unit.PreCombat(unit);
        foreach (UnitObject unit in UnitObject.FilterAlive(AvailablePUnits))
            unit.unit.PreCombat(unit);
    }
    /// <summary>
    /// Checks for win and lose conditions, 
    /// </summary>
    private void CheckWinLoseConditions()
    {
        if (AvailableOUnits.Where(x => !x.unit.isDead).Count() == 0)
        {
            ConcludeBattleWin();
        }
        if (AvailablePUnits.Where(x => !x.unit.isDead).Count() == 0)
        {
            ConcludeBattleLose();
        }
    }
    private (UnitObject,bool) UnitSelection()
    {
        UnitObject playerUnit = null;
        foreach (UnitObject unit in AvailablePUnits.Where(x=>!x.unit.isDead))
        {
            if (!unit.unit.isDead && (playerUnit == null || playerUnit.Recoil > unit.Recoil))
            {
                playerUnit = unit;
            }
        }
        UnitObject opponentUnit = null;
        foreach (UnitObject unit in AvailableOUnits.Where(x => !x.unit.isDead))
        {
            if (!unit.unit.isDead && (opponentUnit == null || opponentUnit.Recoil > unit.Recoil))
                opponentUnit = unit;
        }
        if (opponentUnit.Recoil < playerUnit.Recoil)
        {
            return (opponentUnit, false);
        }
        else
        {
            return (playerUnit, true);
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
                state = BattleState.BattleStep;
                CheckWinLoseConditions();
                UnitObject unitObject;
                bool playerTurn;
                (unitObject, playerTurn) = UnitSelection(); 
                Debug.Log($"{unitObject.unit.name} is acting");
                int recoil = unitObject.Recoil;
                foreach (UnitObject unit in AvailableOUnits)
                {
                    unit.Recoil -= recoil;
                }
                foreach (UnitObject unit in AvailablePUnits)
                {
                    unit.Recoil -= recoil;
                }
                if (!playerTurn)
                {
                    await unitObject.unit.PreAI(AvailableOUnits, AvailablePUnits);
                    selectedaction = unitObject.unit.Ai(AvailableOUnits, AvailablePUnits);
                    UnitObject[] selectTarget = selectedaction.GetTargets(unitObject, unitObject.Allies, unitObject.Opponents).ToArray();
                    await selectedaction.PreAction(unitObject);
                    unitObject.unit.PreAction.Invoke(unitObject);
                    foreach (UnitObject unit in selectTarget)
                    {
                        await selectedaction.DoAction(unitObject, unit);
                        unit.SetSprite(0);
                    }
                    await selectedaction.PostAction(unitObject);
                    unitObject.unit.PostAction.Invoke(unitObject);
                    await unitObject.goBack(selectedaction.returnspeed);
                    unitObject.Recoil += selectedaction.recoil;
                    await unitObject.unit.PostAI(AvailableOUnits, AvailablePUnits);
                }
                else
                {
                    state = BattleState.ActionSelection;
                    List<GameObject> buttons = new List<GameObject>();
                    buttons.Add(Instantiate(buttonPrefab, buttonsBackGround));
                    buttons.Last().transform.localScale = new Vector3(0.5f, 1, 1);
                    buttons.Last().GetComponent<ActionSelectButton>().ActionName.transform.localScale = new Vector3(2, 1, 1);
                    buttons.Last().transform.localPosition = new Vector3(20, 10, 0);
                    buttons.Last().GetComponent<ActionSelectButton>().Setup(new Wait(), this, unitObject,unitObject.Allies,unitObject.Opponents);
                    for (int i = 0; i < unitObject.unit.Actions.Count; i++)
                    {
                        buttons.Add(Instantiate(buttonPrefab,buttonsBackGround));
                        buttons.Last().transform.localPosition = new Vector3(70+i*60, 10, 0);
                        buttons.Last().GetComponent<ActionSelectButton>().Setup(unitObject.unit.Actions[i], this, unitObject, unitObject.Allies, unitObject.Opponents);
                        Debug.Log(unitObject.unit.Actions[i].name);
                    }
                    selectedaction = null;
                    while (selectedaction == null)
                    {
                        await Task.Yield();
                    }
                    state = BattleState.TargetSelection;
                    foreach(GameObject child in buttons)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                    Debug.Log(selectedaction.name);
                    PossibleTargets = selectedaction.GetPossibleTargets(unitObject, unitObject.Allies, unitObject.Opponents).ToArray();
                    List<GameObject> Pointers = new List<GameObject>();
                    foreach (UnitObject unit in PossibleTargets)
                    {
                        Pointers.Add(Instantiate(PointerPrefab,unit.transform));
                        unit.pointer = Pointers.Last();
                        Pointers.Last().transform.localPosition = Vector3.back;
                        Pointers.Last().GetComponent<PointerScript>().SetState(false);
                    }
                    maxU = selectedaction.GetTargets(unitObject, unitObject.Allies, unitObject.Opponents).Count();
                    UpdateTextBox($"Remaining targets: {maxU}");
                    while (targetedUnits.Count<maxU)
                    {
                        await Task.Yield();
                    }
                    UpdateTextBox();
                    foreach (GameObject child in Pointers)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                    await selectedaction.PreAction(unitObject);
                    unitObject.unit.PreAction.Invoke(unitObject);
                    foreach (UnitObject unit in targetedUnits)
                    {
                        await selectedaction.DoAction(unitObject, unit);
                        unit.SetSprite(0);
                    }
                    await selectedaction.PostAction(unitObject);
                    unitObject.unit.PostAction.Invoke(unitObject);
                    await unitObject.goBack(selectedaction.returnspeed);
                    unitObject.Recoil += selectedaction.recoil;
                    targetedUnits = new List<UnitObject>();
                }
                foreach(Action action in unitObject.unit.Actions)
                    if(action.cooldown>0)
                        action.cooldown--;
                await Task.Delay(10);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }
    public void UpdateTextBox(string text = "")
    {
        textbox.text = text;
        LayoutRebuilder.ForceRebuildLayoutImmediate(textbox.rectTransform);
    }
    /// <summary>
    /// This function Concludes Battle For Winning
    /// </summary>
    private void ConcludeBattleWin()
    {
        int sumlevel = AvailableOUnits.Select(x => x.unit.level + 1).Sum();
        MainPlayerCombat player = FindObjectOfType<MainPlayerCombat>();
        Debug.Log(player.UnitLvXp.Count);
        for (int i = 0; i < player.UnitLvXp.Count; i++)
        {
            player.UnitXp[i] += sumlevel;//*GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().level;
            while (player.UnitLvXp[i] < player.UnitXp[i])
            {
                player._playerUnits[i].Setlevel(player._playerUnits[i].level + 1);
                player.UnitXp[i] -= player.UnitLvXp[i];
                player.UnitLvXp[i] *= 2;
            }
        }
        foreach (UnitObject unit in UnitObject.FilterAlive(AvailablePUnits))
        {
            unit.unit.PostCombat(unit);
        }
        foreach (UnitObject unit in AvailablePUnits)
        {
            while (unit.statuses.Count > 0)
            {

                Debug.Log(unit.name);
                unit.statuses[0].RemoveStatus(unit);
            }
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerMovement>().enabled = true;

        GameObject.FindGameObjectWithTag("PlayerEventSystem").GetComponent<EventSystem>().enabled = true;

        GameObject.FindGameObjectWithTag("MiniMap").GetComponent<Canvas>().enabled = true;

        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothTrackChange("Combat", "Peaceful", 0.8f, -1);
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().UnloadScene_Special("CombatScene");
    }

    /// <summary>
    /// This function Concludes Battle For Losing
    /// </summary>
    private void ConcludeBattleLose()
    {
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothFadeAllTracks();
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadScene_NoLoadingScreen("MainMenu");
        SceneManager.UnloadSceneAsync("Generation");
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
