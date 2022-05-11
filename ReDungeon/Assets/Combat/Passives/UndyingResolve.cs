using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyingResolve : Passive
{
    bool ready;
    public UndyingResolve()
    {
        triggers.Add(TriggerType.LateGetHurt); 
        triggers.Add(TriggerType.PreAction);
        description = "instead of Dying, remove 1 maximum hp for each 5 points of damage you would recieve";
        name = "Undying Resolve";
        hidden = true;
        ready = false;
    }
    public override void LateGetHurt(UnitObject bearer, ref int damage)
    {
        if (bearer.unit.CurrentHP <= damage && bearer.unit.maxHP > damage / 5.0)
        {
            if (hidden)
            {
                ready = true;
            }
            else
            {
                bearer.unit.maxHP -= (damage - bearer.unit.CurrentHP + 1) / 5;
            }
            damage = bearer.unit.CurrentHP - 1;
        }
    }

    public override void PreAction(UnitObject bearer)
    {
        if(ready)
            hidden = false;
    }
}
