using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyingResolve : Passive
{
    bool ready;
    int overflowDamage = 0;
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
        bearer.Recoil -= 7;
        if (bearer.unit.CurrentHP <= (damage + overflowDamage) && bearer.unit.maxHP > (damage + overflowDamage) / 5.0)
        {
            if (hidden)
            {
                ready = true;
            }
            else
            {
                bearer.unit.maxHP -= ((damage + overflowDamage) - bearer.unit.CurrentHP + 1) / 5;
                overflowDamage = (damage + overflowDamage) % 5;
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
