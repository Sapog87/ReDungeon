using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glaive : Status
{
    public int stack = 0;

    public Glaive(int stack)
    {
        name = "Glaive";
        this.stack = stack;
        description = $"The bearer can use signature move \"The Glaive\" until it breaks";
    }
    public override void OnSelfApply(UnitObject bearer)
    {
        bearer.unit.Actions.Add(new TheGlaive(this));
    }
    public override void OnClear(UnitObject bearer)
    {
        if(bearer.unit.HasAction("The Glaive"))
        {
            bearer.unit.Actions.Remove(bearer.unit.GetAction("The Glaive"));
        }
    }
    public override void RenewStatus(Status self)
    {
        stack += ((Glaive)self).stack+1;
    }
    public override string ToString()
    {
        return base.ToString() + "\nDurability: " + stack;
    }
}
