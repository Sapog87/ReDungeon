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
    public override bool IsAvailable(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return true;
    }
    public override IEnumerable<UnitObject> GetPossibleTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return new UnitObject[] { };
    }
    public override IEnumerable<UnitObject> GetTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return new UnitObject[] { };
    }
}
