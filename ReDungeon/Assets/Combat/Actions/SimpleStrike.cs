using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SimpleStrike : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        await acter.approach(target.transform, 0.95f, 0.1f);
        await acter.unit.Strike(target, 10, 20);
        await acter.goBack(0.05f);
    }
    public override void SetDefaults()
    {
        recoil = 20;
        name = "Simple Strike";
        description = "Strikes one target for 10-20 base damage";
    }

    public override UnitObject[] GetTargets(UnitObject acter, UnitObject[] allies, UnitObject[] opponents)
    {
        return new UnitObject[]{opponents[Random.Range(0, opponents.Length)]};
    }
}
