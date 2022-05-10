using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom : Status
{
    public int stack = 0;

    public Venom(int stack)
    {
        name = "Venom";
        this.stack = stack;
        triggers.Add(TriggerType.PostAction);
        description = $"The bearer takes damage equal to the stack each turn, then remove 1/2 of the stack";
    }
    public override void RenewStatus(Status self)
    {
        stack += ((Venom)self).stack;
    }
    public override void PostAction(UnitObject bearer)
    {
        bearer.unit.GetHurt(stack);
        stack = Mathf.FloorToInt(stack / 2.0f);
        Debug.Log($"Venom {bearer.unit.name} {stack}");
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
