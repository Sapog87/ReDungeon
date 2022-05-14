using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword :Status
{
    public int stack = 0;

public Sword(int stack)
{
    name = "Sword";
    this.stack = stack;
    description = $"The bearer can use signature move \"The Sword\" until it breaks";
}
public override void OnSelfApply(UnitObject bearer)
{
    bearer.unit.Actions.Add(new TheSword1(this));
    bearer.unit.Actions.Add(new TheSword2(this));
}
public override void OnClear(UnitObject bearer)
{
    if (bearer.unit.HasAction("The Sword2"))
    {
        bearer.unit.Actions.Remove(bearer.unit.GetAction("The Sword2"));
    }
    if (bearer.unit.HasAction("The Sword2"))
    {
        bearer.unit.Actions.Remove(bearer.unit.GetAction("The Sword2"));
    }
    }
public override void RenewStatus(Status self)
{
    stack += ((Sword)self).stack + 1;
}
public override string ToString()
{
    return base.ToString() + "\nDurability: " + stack;
}
}
