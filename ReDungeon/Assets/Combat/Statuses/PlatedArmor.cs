using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatedArmor : Status
{
    public int stack = 0;

    public PlatedArmor(int stack)
    {
        name = "Acid";
        this.stack = stack;
        triggers.Add(TriggerType.PostAction);
        description = $"The bearer takes damage equal to the stack each turn, then remove 1/3 of the stack";
    }
    public override void RenewStatus(Status self)
    {
        stack += ((Acid)self).stack;
    }
    public override void PostAction(UnitObject bearer)
    {
        bearer.unit.GetHurt(stack);
        stack = Mathf.FloorToInt(stack / 3.0f * 2);
        Debug.Log($"Acid {bearer.unit.name} {stack}");
        if (stack < 1)
        {
            RemoveStatus(bearer);
        }
    }
    public override string ToString()
    {
        return base.ToString() + "\nStack: " + stack;
    }
}
