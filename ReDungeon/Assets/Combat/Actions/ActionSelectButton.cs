using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelectButton : MonoBehaviour
{
    [HideInInspector]
    public BattleManager Manager;
    public Action RepresentedAction;
    public Text ActionName;
    public Image image;
    public bool Available;
    public void OnClick()
    {
        if (Available)
        {
            Manager.selectedaction = RepresentedAction;
            Manager.UpdateTextBox();
        }
    }

    private void OnMouseEnter()
    {
        Manager.UpdateTextBox($"{RepresentedAction.description}{(Available?"":$"\n{(RepresentedAction.cooldown>0?$"Cooldown = {RepresentedAction.cooldown}":"No Available Targets")}")}");
    }

    private void OnMouseOver()
    {
    }

    private void OnMouseExit()
    {
        Manager.UpdateTextBox();
    }

    public void Setup(Action action, BattleManager manager, UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        RepresentedAction = action;
        Manager = manager;
        ActionName.text = RepresentedAction.name;
        Available = action.IsAvailable(acter, allies, opponents);
        Sprite timage = Resources.Load<Sprite>(RepresentedAction.ImageReference);
        if (timage != null)
        {
            image.sprite = timage;
            //image = Instantiate(timage);
        }
            
        if (Available)
        {
            image.color = Color.white;
        }
        else
        {
            image.color = Color.gray;
        }
    }
}
