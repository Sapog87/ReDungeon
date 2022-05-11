using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Status
{
    public int stack = 0;

    public Spear(int stack)
    {
        name = "Spear";
        this.stack = stack;
        description = $"The bearer can use signature move \"The Glaive\" until it breaks";
    }
    public override void OnSelfApply(UnitObject bearer)
    {
        bearer.unit.Actions.Add(new TheSpear(this));
    }
    public override void OnClear(UnitObject bearer)
    {
        if (bearer.unit.HasAction("The Spear"))
        {
            bearer.unit.Actions.Remove(bearer.unit.GetAction("The Spear"));
        }
    }
    public override void RenewStatus(Status self)
    {
        stack += ((Spear)self).stack + 1;
    }
    public override string ToString()
    {
        return base.ToString() + "\nDurability: " + stack;
    }
}
