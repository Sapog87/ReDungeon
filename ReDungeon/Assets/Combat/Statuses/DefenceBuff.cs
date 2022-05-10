using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBuff : Status
{
    public int stack = 0;
    int bonusDef = 0;

    public DefenceBuff(int stack)
    {
        name = "Defence Up";
        this.stack = stack;
        triggers.Add(TriggerType.OnAction);
        description = "the bearer gets 35% bonus defence";
    }

    public override void RenewStatus(Status self)
    {
        stack = ((DefenceBuff)self).stack;
    }

    public override void OnSelfApply(UnitObject bearer)
    {
        bonusDef = bearer.unit.defence / 100 * 35;
        bearer.defence += bonusDef;
    }

    public override void OnClear(UnitObject bearer)
    {
        bearer.defence -= bonusDef;
    }

    public override void OnAction(UnitObject bearer, UnitObject target)
    {
        stack--;
        if (stack < 1)
        {
            RemoveStatus(bearer);
        }
    }
}
