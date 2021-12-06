using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skip : Action
{
    public Skip()
    {
        Name = "Skip";
        TargetType = TargetType.ONE;
        CDType = CDType.none;
        MaxUses = -1;
        MaxCooldown = -1;
        ResetCooldown();
    }

    public override List<Unit> GetValidTargets(Unit User)
    {
        return new List<Unit> { User };
    }
}
