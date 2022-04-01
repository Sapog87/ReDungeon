using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Wait : Action
{
    public override void SetDefaults()
    {
        recoil = 10;
        name = "Wait";
        description = "Skips turn";
    }
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        await Task.Delay(500);
    }

    public override UnitObject[] GetTargets(UnitObject acter, UnitObject[] allies, UnitObject[] opponents)
    {
        return new UnitObject[] { };
    }
}
