using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : Status
{
    public int stack = 0;
    
    public Acid(int stack)
    {
        this.stack = stack;
        triggers.Add(TriggerType.OnAction);
        description = "the bearer takes damage equal to the stack each turn, then remove 1/3 of the stack";
    }
    public override void RenewStatus(Status self)
    {
        stack += ((Acid)self).stack;
    }
    public override void OnAction(UnitObject bearer, UnitObject target, int number)
    {
        bearer.unit.GetHurt(stack);
        stack = Mathf.FloorToInt(stack / 3.0f * 2);
        Debug.Log($"Acid {bearer.unit.name} {stack}");
        if(stack < 1)
        {
            RemoveStatus(bearer);
        }
    }

}
