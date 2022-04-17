using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelectButton : MonoBehaviour
{
    public BattleManager manager;
    public Action RepresentedAction;
    public Text ActionName;
    public GameObject PopupPrefab;
    [SerializeField]
    private GameObject popup;
    public void OnClick()
    {
        manager.selectedaction = RepresentedAction;
        manager.textbox.text = "";
        LayoutRebuilder.ForceRebuildLayoutImmediate(manager.textbox.rectTransform);
    }

    private void OnMouseEnter()
    {
        manager.textbox.text = RepresentedAction.description;
        LayoutRebuilder.ForceRebuildLayoutImmediate(manager.textbox.rectTransform);
    }

    private void OnMouseOver()
    {
    }

    private void OnMouseExit()
    {
        manager.textbox.text = "";
        LayoutRebuilder.ForceRebuildLayoutImmediate(manager.textbox.rectTransform);
    }

    public void Setup(Action action, BattleManager manager)
    {
        RepresentedAction = action;
        this.manager = manager;
        ActionName.text = RepresentedAction.name;
    }
}
