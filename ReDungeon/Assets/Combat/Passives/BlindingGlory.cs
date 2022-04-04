using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindingGlory : Passive
{
    public BlindingGlory()
    {
        triggers.Add(TriggerType.OnStrike);
        description = "on hit increases recoil by 5 and decreases own recoil by 5";
        name = "Blinding Glory";
    }
    public override void OnStrike(UnitObject bearer, UnitObject target, int damage)
    {
        Debug.Log("Blinding Glory");
        target.recoil += 5;
        bearer.recoil -= 5;
    }
}
