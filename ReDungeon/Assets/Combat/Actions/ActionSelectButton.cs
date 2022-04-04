using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelectButton : MonoBehaviour
{
    public BattleManager manager;
    public Action RepresentedAction;
    public Text ActionName;
    public void OnClick()
    {
        manager.selectedaction = RepresentedAction;
    }
    public void Setup(Action action, BattleManager manager)
    {
        RepresentedAction = action;
        this.manager = manager;
        ActionName.text = RepresentedAction.name;
    }
}
