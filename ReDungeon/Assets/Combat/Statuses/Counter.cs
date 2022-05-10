using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Status
{
    public int stack = 0;

    public Counter(int stack)
    {
        name = "Counter";
        this.stack = stack;
        triggers.Add(TriggerType.OnTakeHit);
        description = "the bearer counterattacks when get hit";
    }
    public override void RenewStatus(Status self)
    {
        stack = ((Counter)self).stack;
    }

    /*
    async public override void OnTakeHit(UnitObject bearer, UnitObject attacker, int damage)
    {
        await bearer.approach(attacker.transform, 0.95f, 0.05f);
        await bearer.unit.Strike(attacker, 10, 15);
        await bearer.goBack(0.1f);
        stack--;
        if (stack < 1)
        {
            RemoveStatus(bearer);
        }
    }
    */
    
}
