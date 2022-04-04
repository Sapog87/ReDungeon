using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SimpleLick : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        await acter.approach(target.transform, 0.95f, 0.05f);
        await acter.unit.Strike(target, 5, 15);
        target.unit.RecieveStatus(acter, new Acid(3));
        await acter.goBack(0.05f);
    }
    public override void SetDefaults()
    {
        recoil = 25;
        name = "Simple Lick";
        description = "Licks one target for 5-15 base damage and applies 3 stacks of Acid";
    }

    public override UnitObject[] GetTargets(UnitObject acter, UnitObject[] allies, UnitObject[] opponents)
    {
        return new UnitObject[] { opponents[Random.Range(0, opponents.Length)] };
    }
}
