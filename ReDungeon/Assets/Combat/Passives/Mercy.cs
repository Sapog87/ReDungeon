using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercy : Passive
{
    public Mercy()
    {
        triggers.Add(TriggerType.PostCombat);
        description = "After Combat Heals every ally for 10 health";
        name = "Mercy";
    }
    public override void PostCombat(UnitObject bearer)
    {
        Debug.Log("Mercy");
        foreach (UnitObject unit in bearer.Allies)
            unit.unit.CurrentHP += 10;
    }
}
